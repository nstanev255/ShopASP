using Microsoft.AspNetCore.Identity;
using ShopASP.Areas.Identity.Exception;
using ShopASP.Areas.Identity.Models;

namespace ShopASP.Areas.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<IAuthenticationService> _logger;

    public AuthenticationService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        ILogger<AuthenticationService> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Register(RegisterInput model)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("Successfully created a user");
            await _signInManager.SignInAsync(user, isPersistent:false);
        }
        else
        {
            throw new IdentityException(result.Errors);
        }
    }

    public IdentityUser FindUserByUsername(string username)
    {
        var user = _userManager.Users.First(user => user.UserName == username);

        if (user == null)
        {
            throw new System.Exception("User not found.");
        }

        return user;
    }

    public async Task LoginUser(LoginInput loginInput)
    {
        var result = await _signInManager.PasswordSignInAsync(loginInput.Email, loginInput.Password,
            loginInput.RememberMe, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new IdentityException("Something went wrong, try logging in again..");
        }
    }

}