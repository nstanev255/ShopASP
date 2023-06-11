using ShopASP.Models.Entity;

namespace ShopASP.Models;

public class ProductListViewModel
{
    public List<Product> Products { get; set; }
    public int AllPages { get; set; }
    public List<Genre> Genres { get; set; }

    public int CurrentPage { get; set; }
}