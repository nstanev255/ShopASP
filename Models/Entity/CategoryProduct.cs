namespace ShopASP.Models.Entity;

public class CategoryProduct : Base
{
    public Category Category { get; set; }
    public Product Product { get; set; }
}