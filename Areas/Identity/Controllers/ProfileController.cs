using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Areas.Identity.Models;
using ShopASP.Services;

namespace ShopASP.Areas.Identity.Controllers;

[Area("Identity")]
[Route("profile")]
public class ProfileController : Controller
{
    private readonly IOrderService _orderService;

    public ProfileController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [Route("profile-page")]
    public IActionResult ProfilePage(int page = 0)
    {
        if (page <= 1)
        {
            page = 1;
        }

        var orders = _orderService.FindAllPaginate(page);
        var allOrders = _orderService.CountAll();
        var allPages = Utils.PaginationUtils.CalculatePageNumber(allOrders);

        var model = new AccountOrdersViewModel { AllPages = allPages, Orders = orders, CurrentPage = page };

        return View("Profile", model);
    }
}