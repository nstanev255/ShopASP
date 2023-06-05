using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public class DeveloperService : IDeveloperService
{
    private DbSet<Developer> _developerDao;

    public DeveloperService(ApplicationDbContext context)
    {
        _developerDao = context.Developers;
    }

    public Developer? FindOneById(int id)
    {
        return _developerDao.FirstOrDefault(d => d.Id == id);
    }
}