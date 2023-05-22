using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public class PlatformService : IPlatformService
{
    private readonly DbSet<Platform> _platformsDao;

    public PlatformService(ApplicationDbContext dbContext)
    {
        _platformsDao = dbContext.Platforms;
    }
    public List<Platform> FindAll()
    {
        return _platformsDao.ToList();
    }
}