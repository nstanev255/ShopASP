using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASP.Models;

namespace ShopASP.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<ProductModel> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}