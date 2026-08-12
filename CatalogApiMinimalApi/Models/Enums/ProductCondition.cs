namespace CatalogApiMinimalApi.Models.Enums;

/// <summary>
/// Represents the condition of a product.
/// This enumeration can be used to categorize/classify products based on their condition.
/// </summary>
public enum ProductCondition
{
    /// <summary>
    /// Indicates that the product is in fair condition.
    /// This signifies that the product may have noticeable wear and tear or defects, but remains usable and functional. Buyers should expect visible imperfections and reduced overall quality compared to better condition classifications.
    /// </summary>
    Fair,

    /// <summary>
    /// Indicates that the product is in good condition.
    /// This means the product shows minor signs of use but is well-maintained, fully functional, and free from significant defects or damage.
    /// </summary>
    Good,

    /// <summary>
    /// Indicates that the product is in like-new condition.
    /// This signifies that the product has been gently used with minimal, if any, visible signs of wear.
    /// It is nearly indistinguishable from a brand-new product in terms of both appearance and functionality.
    /// </summary>
    LikeNew,

    /// <summary>
    /// Represents a product that is in brand-new condition.
    /// This indicates the item is completely unused, flawless, and in the same state as it was when manufactured or originally packaged. Buyers can expect the highest quality with no signs of prior use or wear.
    /// </summary>
    New,

    /// <summary>
    /// Indicates that the product is in poor condition.
    /// This classification denotes that the product may have significant damage  or defects, reducing its usability or functionality. Buyers should expect  extensive wear and diminished performance or aesthetic value.
    /// </summary>
    Poor,

    /// <summary>
    /// Indicates that the product is in very good condition.
    /// This classification suggests the product is in great shape with minimal signs of use or wear.
    /// It functions properly and may have only minor, barely noticeable cosmetic imperfections.
    /// </summary>
    VeryGood
}