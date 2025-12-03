using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Nmae cant't be blank")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email cant't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [Remote(action: "IsEmailAlreadyRegisterd", controller: "Account", ErrorMessage = "Email is already used")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number cant't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "'Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password cant't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password cant't be blank")]
        [Compare("Password", ErrorMessage = "Password does not match confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
