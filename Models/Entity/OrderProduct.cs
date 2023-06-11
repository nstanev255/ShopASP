namespace ShopASP.Models.Entity;

public class OrderProduct : Base
{
    public Product Product { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
}