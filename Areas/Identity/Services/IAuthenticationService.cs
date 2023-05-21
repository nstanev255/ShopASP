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
    public void LoginUser(IdentityUser identityUser);

}