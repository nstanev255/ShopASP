using Microsoft.AspNetCore.Identity;

namespace ShopASP.Areas.Identity.Exception;

public class IdentityException : System.Exception
{
    public IEnumerable<IdentityError> Errors { get; set; }

    public IdentityException()
    {
        
    }

    public IdentityException(string message) : base(message)
    {
        
    }

    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }
    
}