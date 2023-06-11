using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShopASP.Models.Entity;

[Index(nameof(UUID), IsUnique = true)]
public class Order : Base
{
    public string UUID { get; set; }
    public List<OrderProduct> Products { get; set; }
    public IdentityUser User { get; set; }
    public decimal FinalPrice { get; set; }
    public OrderStatus Status { get; set; }
}