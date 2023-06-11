using ShopASP.Models.Entity;

namespace ShopASP.Models;

public class ProductViewModel
{
    public Product Product { get; set; }
    public string ProductId { get; set; }
    public int CategoryId { get; set; }
}