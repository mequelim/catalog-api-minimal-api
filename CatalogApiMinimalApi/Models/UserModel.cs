namespace CatalogApiMinimalApi.Models;

/// <summary>Represents a user within the catalog system, containing login credentials.</summary>
public class UserModel
{
    /// <summary>Gets or sets the user's email address used for authentication.</summary>
    public string? Email { get; set; }

    /// <summary>Gets or sets the user's password. Should be stored securely (e.g., hashed).</summary>
    public string? Password { get; set; }
}