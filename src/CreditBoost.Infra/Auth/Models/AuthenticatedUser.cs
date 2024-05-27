using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditBoost.Infra.Auth.Models;
public interface IAuthenticatedUser
{
    Guid UserId { get; }
    string UserName { get; }
}

public class AuthenticatedUser(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUser
{
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext.User;

    private bool IsAuthenticated => _user is not null && _user.Identity.IsAuthenticated;

    public Guid UserId => GetUserId();
    public string UserName => GetUserName();

    private Guid GetUserId()
    {
        if (IsAuthenticated)
        {
            var sid = _user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid));
            return Guid.Parse(sid.Value);
        }

        return Guid.Empty;
    }

    private string GetUserName()
    {
        if (IsAuthenticated)
        {
            return _user.Identity.Name;
        }

        return string.Empty;
    }
}
