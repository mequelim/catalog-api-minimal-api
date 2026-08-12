using CatalogApiMinimalApi.Models.Enums;
using CatalogApiMinimalApi.Utils.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogApiMinimalApi.Models;

/// <summary>Represents a product entity in the system which contains details about the product, such as its name, description, price, seller, brand, and other attributes.</summary>
/// <remarks>
/// This class uses several data annotations for validation to ensure the integrity of the product data.
/// It includes validations for required properties, string length limits, ranges for numerical values, and specific formatting rules.
/// </remarks>
[Table("products")]
public record Product : IValidatableObject
{
    /// <summary>Gets or sets the unique identifier for the product.</summary>
    /// <remarks>This property serves as the primary key for the Product entity.</remarks>
    [Key]
    public int ProductId { get; set; }

    /// <summary>Gets or sets the name of the product.</summary>
    /// <remarks>
    /// This property is required and should start with a capital letter.
    /// The maximum length is 255 characters.
    /// </remarks>
    [Required(ErrorMessage = "Your product needs to have a name!")]
    [FirstCapitalLetter]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>Gets or sets the description of the product.</summary>
    /// <remarks>This property provides detailed information about the product, offering additional context or specifications.</remarks>
    [Required(ErrorMessage = "Put a description about your product is necessary!")]
    [FirstCapitalLetter]
    [StringLength(1000)]
    [BindNever]
    public required string Description { get; set; }

    /// <summary>Gets or sets the price of the product.</summary>
    /// <remarks>This property defines the cost of the product and must be a value greater than zero.</remarks>
    [Required(ErrorMessage = "How will someone buy your product if they don't know the price? It's better to give a price!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero!")]
    public required decimal Price { get; set; }

    /// <summary>Gets or sets the name of the seller associated with the product.</summary>
    /// <remarks>This property represents the entity or individual selling the product, and it is required for identifying the seller information related to a product.</remarks>
    [Required(ErrorMessage = "Seller information is required!")]
    [StringLength(255)]
    public required string Seller { get; set; }

    /// <summary>Gets or sets the brand associated with the product.</summary>
    /// <remarks>This property specifies the manufacturer or company responsible for the product.</remarks>
    [Required(ErrorMessage = "Brand information is required!")]
    [FirstCapitalLetter]
    [StringLength(255)]
    public required string Brand { get; set; }

    /// <summary>Gets or sets the model name or identifier associated with the product.</summary>
    /// <remarks>This property provides additional details specific to the product's model, aiding in product differentiation.</remarks>
    [StringLength(255)]
    [BindNever]
    public string? Model { get; set; } = string.Empty;

    /// <summary>Gets or sets the color of the product.</summary>
    /// <remarks>This property specifies the primary color associated with the product, helping to identify its appearance or design.</remarks>
    [Required(ErrorMessage = "Color information is necessary!")]
    [StringLength(80)]
    public required string Color { get; set; }

    /// <summary>Gets or sets the size of the product.</summary>
    /// <remarks>
    /// This property represents the physical or dimensional size associated with the product.
    /// A value greater than zero is required.
    /// </remarks>
    [Required(ErrorMessage = "Size is required!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Size must be greater than zero!")]
    [BindNever]
    public required decimal Size { get; set; }

    /// <summary>Gets or sets the URL of the product's image.</summary>
    /// <remarks>
    /// This property specifies the location of an image that visually represents the product.
    /// It is a required field with a maximum length constraint.
    /// </remarks>
    [Required(ErrorMessage = "You need to inform an image...")]
    [StringLength(1000)]
    [BindNever]
    public required string ImageUrl { get; set; } = string.Empty;

    /// <summary>Gets or sets the quantity of the product currently in stock.</summary>
    /// <remarks>This property ensures that the stock level is maintained and must be greater than or equal to zero.</remarks>
    [Required(ErrorMessage = "The quantity in stock is essential!")]
    // [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock cannot be negative!")]
    [BindNever]
    public required int QuantityInStock { get; set; }

    /// <summary>Gets or sets the condition of the product.</summary>
    /// <remarks>This property indicates the state or quality of the product as defined by the <see cref="ProductCondition"/> enumeration.</remarks>
    [Required(ErrorMessage = "Condition information is essential!")]
    [EnumDataType(typeof(ProductCondition), ErrorMessage = "Invalid product condition.")]
    public required ProductCondition Condition { get; set; }

    /// <summary>Gets or sets the status of the product.</summary>
    /// <remarks>Indicates the current state of the product, such as Available, Discontinued, OutOfStock, or PreOrder.</remarks>
    [Required(ErrorMessage = "Status information is essential!")]
    [EnumDataType(typeof(ProductStatus), ErrorMessage = "Invalid product status.")]
    [BindNever]
    public required ProductStatus Status { get; set; }

    /// <summary>Gets or sets the date and time when the product was created.</summary>
    /// <remarks>
    /// This property reflects the timestamp of the product's creation in the system.
    /// By default, it is initialized to the current UTC date and time.
    /// </remarks>
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    /// <summary>Gets or sets the date and time when the product was last updated.</summary>
    /// <remarks>This property is used to track the last modification timestamp of the product entry.</remarks>
    public DateTime UpdatedAt { get; } = DateTime.UtcNow;

    /// <summary>Gets or sets the unique identifier for the category associated with the product.</summary>
    /// <remarks>This property is used to link the product to its corresponding category in the system.</remarks>
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive number.")]
    public int CategoryId { get; set; }

    /// <summary>Gets or sets the category associated with the product.
    /// </summary>
    /// <remarks>
    /// This property establishes a relationship between the product and its respective category.
    /// It is used to group products under a specific category.
    /// </remarks>
    [JsonIgnore]
    public Category? Category { get; set; }

    /// <summary>Validates the properties of the Product instance to ensure they meet the defined criteria.</summary>
    /// <param name="validationContext">The context information about the object being validated.</param>
    /// <returns>A collection of <see cref="ValidationResult"/> objects that describe any validation failures.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        /* if(!string.IsNullOrEmpty(Name))
        {
            string firstLetter = Name[0].ToString();

            if(!firstLetter.Equals(firstLetter.ToUpper()))
            {
                yield return new ValidationResult(
                    "The first letter must be UPPERCASE!",
                    new[] { nameof(Name) }
                );
            }
        } */

        if(QuantityInStock < 0)
        {
            yield return new ValidationResult(
                "The quantity in stock must be EQUAL or GREATER than 0 (zero)!",
                new[] { nameof(QuantityInStock) }
            );
        }
    }
}