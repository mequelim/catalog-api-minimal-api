namespace CatalogApiMinimalApi.Models.Enums;

/// <summary>
/// Represents the status of a product in the system.
/// Defines various states a product can be in, such as available, discontinued, or out of stock.
/// </summary>
public enum ProductStatus
{
    /// <summary>
    /// Indicates that the product is available for purchase.
    /// Represents a state where the product is in stock and can be sold immediately without restrictions.
    /// </summary>
    Available,

    /// <summary>
    /// Indicates that the product has been permanently removed from availability.
    /// Represents a state in which the product is no longer sold or produced.
    /// </summary>
    Discontinued,

    /// <summary>
    /// Indicates that the product is currently out of stock and unavailable for purchase.
    /// Represents a state where the product cannot be sold due to the absence of inventory.
    /// </summary>
    OutOfStock,

    /// <summary>
    /// Indicates that the product is available for pre-order.
    /// Represents a state where the product is not yet released but can be ordered in advance.
    /// </summary>
    PreOrder
}