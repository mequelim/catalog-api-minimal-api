using CatalogApiMinimalApi.Models;
using CatalogApiMinimalApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogApiMinimalApi.Services;

/// <summary>Service responsible for generating JWT tokens for authenticated users.</summary>
public class TokenService : ITokenService
{
    /// <summary>Generates a JWT token for the specified user with the provided configuration.</summary>
    /// <param name="key">The secret key used to sign the token.</param>
    /// <param name="issuer">The issuer of the token (usually your application or authentication server).</param>
    /// <param name="audience">The intended audience of the token (typically your API or services).</param>
    /// <param name="tokenValidationTime">The duration in minutes for which the token remains valid.</param>
    /// <param name="user">The user model containing information to embed within the token claims.</param>
    /// <returns>A signed JWT token as a string.</returns>
    public string GenerateToken(string key, string issuer, string audience, int tokenValidationTime, UserModel user)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
        };

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(tokenValidationTime), signingCredentials: credentials);
        JwtSecurityTokenHandler tokenHandler = new();
        string stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}