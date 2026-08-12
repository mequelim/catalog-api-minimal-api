using CatalogApiMinimalApi.Models;
using CatalogApiMinimalApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CatalogApiMinimalApi.Context;

/// <summary>Represents the application's database context, providing access to the database tables and enabling CRUD operations for the application's entities.</summary>
/// <remarks>
/// This class is derived from <see cref="DbContext"/> and serves as the primary entry point for interacting with the application's database.
/// It includes <see cref="DbSet{TEntity}"/> properties for each entity type that needs to be mapped to a database table.
/// </remarks>
public class AppDbContext : DbContext
{
    /// <summary>Initializes a new instance of the <see cref="AppDbContext"/> class using the specified options.</summary>
    /// <remarks>This constructor is typically used to configure the database context with specific settings, such as the connection string, database provider, or other options defined in <see cref="DbContextOptions"/>.</remarks>
    /// <param name="options">The options to configure the database context. Cannot be null.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //* Mapping the entities to the database tables.
    /// <summary>Maps the <see cref="Category"/> entity classes to their corresponding database tables.</summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>Maps the <see cref="Product"/> entity classes to their corresponding database tables.</summary>
    public DbSet<Product> Products { get; set; }

    //? Methods...
    /// <summary>
    /// Represents the method that is called when the model for a derived context is being created.
    /// When the context is initialized (created), this method is invoked to apply configurations such as entity mappings, relationships, and constraints.
    /// </summary>
    /// <param name="modelBuilder">A <see cref="ModelBuilder"/> instance that provides a way to configure the model and its mappings to the database.</param>
    /// <remarks>
    /// Databases are created based on the model defined in this method.
    /// Databases has their own structure, which is defined by the properties and relationships of the entities.
    /// If we do not define this method, the default conventions will be used to create the database structure and its types, which may not match the desired structure.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //* Mapping the Category entity to the database table...
        modelBuilder.Entity<Category>()
            .HasKey((category) => category.CategoryId);

        modelBuilder.Entity<Category>()
            .Property((category) => category.Name)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .Property((category) => category.Description)
            .HasMaxLength(1000)
            .IsRequired();

        //* Mapping the Product entity to the database table...
        modelBuilder.Entity<Product>()
            .HasKey((product) => product.ProductId);

        modelBuilder.Entity<Product>()
            .Property((product) => product.Name)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Description)
            .HasMaxLength(1000)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Price)
            .HasPrecision(20, 2)  // Precision and scale for decimal values.
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Seller)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Brand)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Brand)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Model)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Color)
            .HasMaxLength(80)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Size)
            .HasPrecision(5, 2)  // Precision and scale for decimal values.
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.ImageUrl)
            .HasMaxLength(1000)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.QuantityInStock)
            .HasPrecision(8, 0)  // Precision and scale for integer values.
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Condition)
            .HasConversion(
                condition => condition.ToString(),  // Convert the enum to a string for storage.
                conditionString => (ProductCondition) Enum.Parse(typeof(ProductCondition), conditionString, true)  // Convert the string back to the enum.
            )  // Convert the enum to a string for storage.
            .IsConcurrencyToken()  // This property is used to handle concurrency conflicts.
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property((product) => product.Status)
            .HasConversion(
                status => status.ToString(),  // Convert the enum to a string for storage.
                statusString => (ProductStatus) Enum.Parse(typeof(ProductStatus), statusString, true)  // Convert the string back to the enum
            )  // Convert the enum to a string for storage.
            .IsConcurrencyToken()  // This property is used to handle concurrency conflicts.
            .IsRequired();

        //* Relationships...
        modelBuilder.Entity<Product>()
            .HasOne<Category>((category) => category.Category)
            .WithMany((product) => product.Products)
            .HasForeignKey((product) => product.CategoryId);
    }
}