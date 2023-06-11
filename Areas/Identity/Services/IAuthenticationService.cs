using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using ShopASP.Areas.Identity.Models;

namespace ShopASP.Areas.Identity.Services;

public interface IAuthenticationService
{
    /**
     * Register a new user.
     */
    public Task Register(RegisterInput model);
    /**
     * Find user by username.
     */
    public IdentityUser FindUserByUsername(string username);
    /**
     * Login a user.
     *
     */
    public Task LoginUser(LoginInput loginInput);

    public Task LogoutUser();

    public IIdentity GetCurrentUser();

}