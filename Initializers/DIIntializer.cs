using ShopASP.Areas.Identity.Services;
using ShopASP.Services;

namespace ShopASP.Initializers;

public static class DIIntializer
{
    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IPlatformService, PlatformService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IDeveloperService, DeveloperService>();
    }
}