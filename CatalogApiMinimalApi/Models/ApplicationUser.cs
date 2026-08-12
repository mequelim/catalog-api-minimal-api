using Microsoft.AspNetCore.Identity;

namespace CatalogApiMinimalApi.Models;

/// <summary>
/// Represents an application user that extends the ASP.NET Core Identity user model.
/// Includes additional properties for managing refresh tokens.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Represents the refresh token assigned to the user, which is used to obtain a new access token when the existing access token has expired.
    /// The value of this property is typically a unique string that is securely stored and transmitted between the client and server.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Specifies the expiration time of the refresh token assigned to the user.
    /// This property is used to determine whether a provided refresh token is still valid, based on a DateTime value indicating the point in time when the token will expire.
    /// </summary>
    public DateTime RefreshTokenExpirationTime { get; set; }
}
