using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Repository;
using KASHOP.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOP.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRespository, CategoryRespository>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();
            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<ICartService, CartService>();
            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICheckoutService, CheckoutService>();
            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IManageUserService, ManageUserService>();
            Services.AddScoped<IReviewService, BLL.Service.ReviewService>();
            Services.AddScoped<IReviewRepository, ReviewRepository>();



        }
    }

}
