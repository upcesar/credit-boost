using CreditBoost.Application.Commands;
using CreditBoost.Application.Responses;
using CreditBoost.Infra.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CreditBoost.Application.CommandHandlers;

public sealed class LoginCommandHandler(UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
    : IRequestHandler<LoginCommand, AuthenticationResponse>
{
    private readonly JwtOptions _jwt = jwtOptions.Value;

    public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AuthenticationResponse response = new();

        var user = await userManager.FindByNameAsync(request.Username);

        if (user is not null && await userManager.CheckPasswordAsync(user, request.Password))
        {
            List<Claim> authClaims = [
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ];

            WriteToken(response, authClaims);
        }

        return response;
    }

    private void WriteToken(AuthenticationResponse response, List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwt.ValidIssuer,
            audience: _jwt.ValidAudience,
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        response.Token = new JwtSecurityTokenHandler().WriteToken(token);
        response.Expiration = token.ValidTo;
        response.IsAuthenticated = true;
    }
}
