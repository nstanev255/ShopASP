using ShopASP.Models;

namespace ShopASP.Services;

public interface IOrderService
{
    public Task PlaceSingleOrder(SingleOrderInputModel inputModel);
    public Task AcceptOrder(string orderId);
    public Task RejectOrder(string orderId);
}