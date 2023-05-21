using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Areas.Identity.Exception;
using ShopASP.Areas.Identity.Models;
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
    
    [Route("[action]")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegisterInput();
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> Register([FromForm] RegisterInput inputModel)
    {
        if (!ModelState.IsValid)
        {
            return View(model: inputModel);
        }

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

    [AllowAnonymous]
    [Route("[action]")]
    public IActionResult Login()
    {
        var model = new LoginInput();
        return View(model: model);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login(LoginInput loginInput)
    {
        if (!ModelState.IsValid)
        {
            return View(model: loginInput);
        }

        try
        {
            await _authenticationService.LoginUser(loginInput);
            var url = Url.Content("~/");
            return LocalRedirect(url);
        }
        catch (IdentityException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(model: loginInput);
        }
    }
}