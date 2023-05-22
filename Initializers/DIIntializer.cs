using ShopASP.Areas.Identity.Services;
using ShopASP.Services;

namespace ShopASP.Initializers;

public static class DIIntializer
{
    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IProductService, ProductService>();
    }
}