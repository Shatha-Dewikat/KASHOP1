using KASHOP.BLL.Service;
using KASHOP.DAL.Repository;
using KASHOP.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOP.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICategoryRespository, CategoryRespository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISeedData, RoleSeedData>();
            services.AddScoped<ISeedData, UserSeedData>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }

}
