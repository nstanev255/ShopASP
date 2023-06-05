using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Initializers;
using ShopASP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;

    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login/";
    options.LogoutPath = "/Identity/Account/Logout/";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Initialize the DI services. 
DIIntializer.Initialize(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(name: "register", "Identity", pattern: "{controller=Account}/{action=Register}/");
app.MapControllerRoute(name: "product", pattern: "{category}");

using (var seviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    try
    {
        ApplicationDbContext context = seviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        IGenreService genreService = seviceScope.ServiceProvider.GetRequiredService<IGenreService>();
        ICategoryService categoryService = seviceScope.ServiceProvider.GetRequiredService<ICategoryService>();
        IDeveloperService developerService = seviceScope.ServiceProvider.GetRequiredService<IDeveloperService>();
        
        DatabaseInitializer.SeedData(context, genreService, categoryService, developerService).Wait();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}


app.Run();