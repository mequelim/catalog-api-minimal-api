using CatalogApiMinimalApi.Models;

namespace CatalogApiMinimalApi.Services.Interfaces;

/// <summary>Provides operations for generating and handling tokens.</summary>
public interface ITokenService
{
    /// <summary>Generates a JWT (JSON Web Token) based on the provided credentials and user information.</summary>
    /// <param name="key">The secret key used to sign the token.</param>
    /// <param name="issuer">The issuer (iss) claim identifies the principal that issued the token.</param>
    /// <param name="audience">The audience (aud) claim identifies the recipients that the token is intended for.</param>
    /// <param name="tokenValidationInMinutes">The token validation time, in minutes.</param>
    /// <param name="user">The user object containing authentication data such as email.</param>
    /// <returns>A signed JWT as a string.</returns>
    string GenerateToken(string key, string issuer, string audience, int tokenValidationInMinutes, UserModel user);
}