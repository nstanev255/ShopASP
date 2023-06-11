using ShopASP.Models;

namespace ShopASP.Services;

public interface IOrderService
{
    public Task PlaceSingleOrder(SingleOrderInputModel inputModel);
}