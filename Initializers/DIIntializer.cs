using ShopASP.Areas.Identity.Services;

namespace ShopASP.Initializers;

public static class DIIntializer
{
    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}