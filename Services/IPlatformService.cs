using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IPlatformService
{
    List<Platform> FindAll();
}