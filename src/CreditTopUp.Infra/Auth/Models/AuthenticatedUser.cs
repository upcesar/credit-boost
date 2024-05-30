using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditTopUp.Infra.Auth.Models;
public interface IAuthenticatedUser
{
    Guid UserId { get; }
    string UserName { get; }
}

public class AuthenticatedUser(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUser
{
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext.User;

    private bool IsAuthenticated => _user is ClaimsPrincipal { Identity.IsAuthenticated: true };

    public Guid UserId => GetUserId();
    public string UserName => GetUserName();

    private Guid GetUserId()
    {
        if (IsAuthenticated)
        {
            var sid = _user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid));
            return Guid.TryParse(sid.Value, out var userId) ? userId : Guid.Empty;
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
