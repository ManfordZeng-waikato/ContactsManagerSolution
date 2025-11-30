using ContacsManager.Infrastructure.DatabaseContext;
using ContacsManager.Infrastructure.Repositories;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Services;
using ContactsManager.UI.Filters.ActionFilters;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            /*services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                options.RequestBodyLogLimit = 4096;
                options.ResponseBodyLogLimit = 4096;
            });
            */
            /*builder.Logging
                   .ClearProviders().AddConsole().AddDebug() ;*/

            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<ResponseHeaderActionFilter>(5);

                options.Filters.Add(new ResponseHeaderFilterFactoryAttribute("My-Key-From-Global", "My-Value-From-Global", 2));
            });

            //add services into IOC container 
            services.AddScoped<ICountriesUploaderService, CountriesUploaderService>();
            services.AddScoped<ICountriesAdderService, CountriesAdderService>();
            services.AddScoped<ICountriesGetterService, CountriesGetterService>();

            services.AddScoped<PersonsGetterService, PersonsGetterService>();
            services.AddScoped<IPersonsGetterService, PersonsGetServiceWithFewExcelFields>();
            services.AddScoped<IPersonsAdderService, PersonsAdderService>();
            services.AddScoped<IPersonsUpdaterService, PersonsUpdaterService>();
            services.AddScoped<IPersonsDeleterService, PersonsDeleterService>();
            services.AddScoped<IPersonsSorterService, PersonsSorterService>();

            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            if (!env.IsEnvironment("Test"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")!);
                });
            }
            services.AddScoped<ResponseHeaderActionFilter>();

            return services;

        }
    }
}
