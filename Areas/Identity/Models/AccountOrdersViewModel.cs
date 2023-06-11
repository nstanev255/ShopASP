using ShopASP.Models.Entity;

namespace ShopASP.Areas.Identity.Models;

public class AccountOrdersViewModel
{
    public int AllPages { get; set; }
    public List<Order> Orders { get; set; }
    public int CurrentPage { get; set; }
}