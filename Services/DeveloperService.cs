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

    public async Task<Developer> FindOneById(int id)
    {
        return await _developerDao.Where(d => d.Id == id).FirstAsync();
    }
}