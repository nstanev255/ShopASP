using Microsoft.EntityFrameworkCore;
using ShopASP.Models;

namespace ShopASP.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProductModel> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base()
    {
        
    }
}