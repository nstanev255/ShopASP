using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASP.Models.Entity;

namespace ShopASP.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<Category?> Categories { get; set; }

    public DbSet<Developer> Developers { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}