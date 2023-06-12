using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Services;

namespace ShopASP.Areas.Cms.Controllers;

[Area("Cms")]
[Route("/cms/")]
public class CmsController : Controller
{
    private readonly IOrderService _orderService;
    public CmsController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [Route("orders")]
    public IActionResult Orders()
    {

        return View();
    }

}