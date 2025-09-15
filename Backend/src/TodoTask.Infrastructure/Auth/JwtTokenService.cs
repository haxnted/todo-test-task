using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Infrastructure.Options;

namespace TodoTask.Infrastructure.Auth;

/// <inheritdoc />
public class JwtTokenService(JwtOptions JwtOptions) : ITokenService
{
    /// <inheritdoc />
    public string GenerateToken(Guid userId, string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(claims: claims,
            expires: DateTime.UtcNow.AddMinutes(JwtOptions.ExpiryMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}