using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IDeveloperService
{
    public Task<Developer?> FindOneById(int id);
}