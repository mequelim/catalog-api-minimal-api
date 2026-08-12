using CatalogApiMinimalApi.Utils.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogApiMinimalApi.Models;

/// <summary>Represents a category entity, containing information about product categories within the catalog system.</summary>
/// <remarks>
/// The Category class is used to define the structure of category data stored in the database.
/// It includes properties for category details such as name, description, creation and update timestamps, and the associated products.
/// </remarks>
[Table("categories")]
public record Category
{
    /// <summary>Represents a category entity, containing information about product categories within the catalog system.</summary>
    /// <remarks>
    /// The Category class is used to define the structure of category data stored in the database.
    /// It includes properties for category details such as name, description, creation and update timestamps, and the associated products.
    /// </remarks>
    public Category() => Products = new Collection<Product>();

    /// <summary>Gets or sets the unique identifier for a category.</summary>
    /// <remarks>This property serves as the primary key for the Category entity and is used to uniquely identify each category.</remarks>
    [Key]
    public int CategoryId { get; set; }

    /// <summary>Gets or sets the name of the category.</summary>
    /// <remarks>
    /// This property represents the name of the category, which is a required field and must start with a capital letter.
    /// It is used to uniquely identify the category within the system's context.
    /// </remarks>
    [Required(ErrorMessage = "Category name is required!")]
    [FirstCapitalLetter]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>Gets or sets the description of the category.</summary>
    /// <remarks>
    /// This property provides detailed information about the category and is intended to describe its purpose or characteristics.
    /// It is mandatory and must start with a capital letter.
    /// </remarks>
    [Required(ErrorMessage = "Category description is required!")]
    [FirstCapitalLetter]
    [StringLength(1000)]
    [BindNever]
    public required string Description { get; set; }

    /// <summary>Gets or sets the date and time when the category was created.</summary>
    /// <remarks>
    /// This property is automatically set to the current UTC date and time when a new category is created.
    /// It helps to track when the category was first added to the catalog system.
    /// </remarks>
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    /// <summary>Gets or sets the timestamp of the last update made to the category.</summary>
    /// <remarks>
    /// This property is automatically set to the current UTC time when a category is updated.
    /// It is useful for tracking the modification history of a category entity in the system.
    /// </remarks>
    public DateTime UpdatedAt { get; } = DateTime.UtcNow;

    /// <summary>Gets or sets the collection of products associated with the category.</summary>
    /// <remarks>
    /// This property defines the relationship between a category and its products,
    /// enabling navigation and data access for products grouped under a particular category.
    /// </remarks>
    [JsonIgnore]
    public ICollection<Product>? Products { get; }  // Navigation property for related products.
}