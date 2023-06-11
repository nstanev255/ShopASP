using ShopASP.Models.Entity;

namespace ShopASP.Models;

public class SingleOrderViewModel
{
    public BasicProduct Product { get; set; }
    public CategoryType CategoryType { get; set; }
    public decimal FinalPrice { get; set; }
}