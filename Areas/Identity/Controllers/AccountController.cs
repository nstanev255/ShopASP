using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Areas.Identity.Exception;
using ShopASP.Areas.Identity.Models;
using ShopASP.Areas.Identity.Services;

namespace ShopASP.Areas.Identity.Controllers;

[Area("Identity")]
[Route("identity/account")]
public class AccountController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAuthenticationService authenticationService, ILogger<AccountController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    [Route("register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegisterInput();
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromForm] RegisterInput inputModel)
    {
        if (!ModelState.IsValid)
        {
            return View(model: inputModel);
        }

        try
        {
            await _authenticationService.Register(inputModel);
            return RedirectToAction("Index", "Home");
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
    [Route("login")]
    public IActionResult Login()
    {
        var model = new LoginInput();
        return View(model: model);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginInput loginInput)
    {
        if (!ModelState.IsValid)
        {
            return View(model: loginInput);
        }

        try
        {
            await _authenticationService.LoginUser(loginInput);
            if (String.IsNullOrEmpty(loginInput.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(loginInput.ReturnUrl);
        }
        catch (IdentityException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(model: loginInput);
        }
    }

    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authenticationService.LogoutUser();
        return RedirectToAction("Index", "Home", new { area = "" });
    }
}