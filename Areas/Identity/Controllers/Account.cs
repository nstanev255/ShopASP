using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Areas.Identity.Exception;
using ShopASP.Areas.Identity.Services;

namespace ShopASP.Areas.Identity.Controllers;

[Area("Identity")]
[Route("identity/[controller]")]
public class Account : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<Account> _logger;
    
    public Account(IAuthenticationService authenticationService, ILogger<Account> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }
    
    // GET
    [Route("[action]")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new Models.RegisterInput();
        return View(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] Models.RegisterInput inputModel)
    {
        _logger.LogInformation("here");
        // if (!ModelState.IsValid)
        // {
        //     // return View(model: inputModel);
        // }

        try
        {
            await _authenticationService.Register(inputModel);
            
            var url= Url.Content("~/");
            return LocalRedirect(url);
        }
        catch (IdentityException exception)
        {
            foreach (var error in exception.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return View(model: inputModel);
        }
    }
}