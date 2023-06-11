using ShopASP.Areas.Identity.Services;
using ShopASP.Configuration;
using ShopASP.Services;

namespace ShopASP.Initializers;

public static class DIIntializer
{
    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IDeveloperService, DeveloperService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddHttpContextAccessor();

        
        builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddScoped<IMailService, MailService>();
    }
}