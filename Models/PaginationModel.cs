namespace ShopASP.Models;

public class PaginationModel
{
    public int CurrentPage { get; set; }
    public int AllPages { get; set; }

    public string Action { get; set; }
    public string Controller { get; set; }
    public object? AdditionalParams { get; set; }
}