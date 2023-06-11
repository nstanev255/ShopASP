using ShopASP.Models.Entity;

namespace ShopASP.Models;

public class SingleOrderViewModel
{
    public BasicProduct Product { get; set; }
    public Category Category { get; set; }
    public decimal FinalPrice { get; set; }

    public SingleOrderInputModel InputModel { get; set; }

}