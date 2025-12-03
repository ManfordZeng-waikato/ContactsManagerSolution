using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                PersonName = registerDTO.PersonName
            };
            IdentityResult result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(registerDTO);
            }
            else
            {
                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new()
                        {
                            Name = UserTypeOptions.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);

                        await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.Admin.ToString());
                    }
                }
                else
                {
                    await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.User.ToString());

                }

                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                return RedirectToAction(nameof(PersonsController.Index), "Persons");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return View(loginDTO);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", "Invalid email or password.");
                return View(loginDTO);
            }
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (applicationUser != null)
            {
                if (await _userManager.IsInRoleAsync(applicationUser, UserTypeOptions.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }

        public async Task<IActionResult> IsEmailAlreadyRegisterd(string email)
        {
            ApplicationUser application = await _userManager.FindByEmailAsync(email);
            if (application == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}