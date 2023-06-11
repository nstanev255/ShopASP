using Microsoft.AspNetCore.Identity;

namespace ShopASP.Models.Entity;

public class Order : Base
{
    public List<OrderProduct> Products { get; set; }
    public IdentityUser User { get; set; }
    public decimal FinalPrice { get; set; }
    public OrderStatus Status { get; set; }
}