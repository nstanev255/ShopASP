using ShopASP.Models;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IOrderService
{
    public Task PlaceSingleOrder(SingleOrderInputModel inputModel);
    public Task AcceptOrder(string orderId);
    public Task RejectOrder(string orderId);
    public List<Order> FindAllPaginate(int page);
    public int CountAll();
}