using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public class CategoryService : ICategoryService
{
    private DbSet<Category?> _dao;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _dao = dbContext.Categories;
    }

    public Category? FindOneById(int id)
    {
        return _dao.FirstOrDefault(c => c.Id == id);
    }
}