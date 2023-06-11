using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopASP.Controllers;

public class OrderController : Controller
{
    private ILogger<OrderController> _logger;
    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;

        
    }

    /**
     * This Action is used so that we can show the order.
     */
    [HttpPost]
    public IActionResult LoadOrder()
    {

        return View();
    }
}