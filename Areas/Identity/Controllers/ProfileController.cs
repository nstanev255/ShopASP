using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Areas.Identity.Models;
using ShopASP.Areas.Identity.Services;
using ShopASP.Services;

namespace ShopASP.Areas.Identity.Controllers;

[Area("Identity")]
[Route("profile")]
public class ProfileController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IAuthenticationService _authentication;

    public ProfileController(IOrderService orderService, IAuthenticationService authenticationService)
    {
        _orderService = orderService;
        _authentication = authenticationService;
    }

    [Authorize]
    [Route("profile-page")]
    public IActionResult ProfilePage(int page = 0)
    {
        if (page <= 1)
        {
            page = 1;
        }
        
        var user = _authentication.FindUserByUsername(User.Identity.Name);
        
        var allOrders = _orderService.CountAllByUserId(user.Id);
        var allPages = Utils.PaginationUtils.CalculatePageNumber(allOrders);

        var orders = _orderService.FindAllPaginateByUser(page, user.Id);
        var model = new AccountOrdersViewModel { AllPages = allPages, Orders = orders, CurrentPage = page };

        return View("Profile", model);
    }
}