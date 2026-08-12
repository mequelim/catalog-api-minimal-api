using CatalogApiMinimalApi.Context;
using CatalogApiMinimalApi.Models;
using CatalogApiMinimalApi.Services;
using CatalogApiMinimalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();  //* It is necessary to Minimal APIs.
builder.Services.AddSwaggerGen();  //* Register `ISwaggerProvider`.

//? Registering token, which will be used in JWT authentication...
builder.Services.AddSingleton<ITokenService>(new TokenService());

//? Registering authentication...
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer((options) =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:SecretKey"))
        };
    });

//? Registering authorization...
builder.Services.AddAuthorization();

//? Registering database (PostgreSQL) connection...
string postgresConnectionString = builder.Configuration.GetConnectionString("DefaultPostgresConnection")
                                  ?? throw new InvalidOperationException("PostgreSQL connection was not found or was not correctly configured!");

//? Registering database context (Entity Framework Core)...
builder.Services.AddDbContext<AppDbContext>((options) => options.UseNpgsql(postgresConnectionString));

WebApplication app = builder.Build();

//? Endpoints...
//! Login:
app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
{
    if(userModel is null) return Results.BadRequest("Invalid login!");

    if(userModel.Email == "pedro@example.com" && userModel.Password == "Abc@123")
    {
        var tokenString = tokenService.GenerateToken(
            app.Configuration["Jwt:SecretKey"]!,
            app.Configuration["Jwt:Issuer"]!,
            app.Configuration["Jwt:Audience"]!,
            Convert.ToInt32(app.Configuration["Jwt:TokenValidationInMinutes"]),
            userModel
        );

        return Results.Ok(new { token = tokenString });
    } else return Results.BadRequest("Invalid login!");
});

//! Categories:
//* GET all:
app.MapGet("/categories", async (AppDbContext database) => await database.Categories.ToListAsync());

//* GET by id:
app.MapGet("/categories/:{id:int}", async (AppDbContext database, int id) =>
{
    if(id <= 0) return Results.NotFound($"The id may be greater than 0 (zero)... you sent the {id}.");

    var category = await database.Categories.FindAsync(id);

    return (category is not null)
        ? Results.Ok(category)
        : Results.NotFound("Category not found!");
});

//* GET by name:
app.MapGet("/categories/{name}", async (AppDbContext database, string name) =>
{
    if(string.IsNullOrWhiteSpace(name)) return Results.BadRequest("You must provide a valid name!");

    var category = await database.Categories.FirstOrDefaultAsync((c) => c.Name == name);

    return (category is not null)
        ? Results.Ok(category)
        : Results.NotFound("Category not found!");
});

//* POST:
app.MapPost("/categories", async (AppDbContext database, Category category) =>
{
    if(category is null) return Results.Content("It was impossible to create this category! Review it, and try again...");

    database.Categories.Add(category);
    await database.SaveChangesAsync();

    return Results.Ok($"Category {category.CategoryId} created successfully!");
});

//* PUT:
app.MapPut("/categories/{id:int}", async (AppDbContext database, Category category, int id) =>
{
    if(category is null) return Results.Content("It was impossible to create this category! Review it, and try again...");
    if(id <= 0) return Results.NotFound($"The id may be greater than 0 (zero)... you sent an id as {id}.");
    if(category.CategoryId != id) return Results.BadRequest();

    var existingCategory = await database.Categories.FindAsync(id);

    if(existingCategory is null) return Results.NotFound($"It is impossible to update a category to NULL data!");

    var updatedCategory = existingCategory with
    {
        Name = category.Name,
        Description = category.Description
    };

    database.Entry(existingCategory).CurrentValues.SetValues(updatedCategory);
    await database.SaveChangesAsync();

    return Results.Ok($"Category {category.CategoryId} updated successfully!");
});

//* DELETE:
app.MapDelete("/categories/{id:int}", async (AppDbContext database, int id) =>
{
    var category = await database.Categories.FindAsync(id);

    if(category is null) return Results.NotFound();

    database.Categories.Remove(category);
    await database.SaveChangesAsync();

    return Results.NoContent();
});

//! Products:
//* GET all:
app.MapGet("/products", async (AppDbContext database) => await database.Products.ToListAsync());

//* GET by id:
app.MapGet("/products/{id:int}", async (AppDbContext database, int id) =>
{
    if(id <= 0) return Results.NotFound($"The id may be greater than 0 (zero)... you sent the id {id}.");

    var product = await database.Products.FindAsync(id);

    return (product is not null)
        ? Results.Ok(product)
        : Results.NotFound("Product not found!");
});

//* GET by name:
app.MapGet("/products/{name}", async (AppDbContext database, string name) =>
{
    if(string.IsNullOrWhiteSpace(name)) return Results.BadRequest("You must provide a valid name!");

    var product = await database.Products.FirstOrDefaultAsync((p) => p.Name == name);

    return (product is not null)
        ? Results.Ok(product)
        : Results.NotFound("Product not found!");
});

//* POST:
app.MapPost("/products", async (AppDbContext database, Product product) =>
{
    if(product is null) return Results.Content("It was impossible to create this product! Review it, and try again...");

    database.Products.Add(product);
    await database.SaveChangesAsync();

    return Results.Ok($"Category {product.ProductId} created successfully!");
});

//* PUT:
app.MapPut("/products/{id:int}", async (AppDbContext database, Product product, int id) =>
{
    if(product is null) return Results.Content("It was impossible to create this product! Review it, and try again...");
    if(id <= 0) return Results.NotFound($"The id may be greater than 0 (zero)... you sent an id as {id}.");
    if(product.ProductId != id) return Results.BadRequest();

    var existingProduct = await database.Products.FindAsync(id);

    if(existingProduct is null) return Results.NotFound($"It impossible update a product to NULL data!");

    var updatedProduct = existingProduct with
    {
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Seller = product.Seller,
        Brand = product.Brand,
        Model = product.Model,
        Color = product.Color,
        Size = product.Size,
        ImageUrl = product.ImageUrl,
        QuantityInStock = product.QuantityInStock,
        Condition = product.Condition,
        Status = product.Status,
        CategoryId = product.CategoryId
    };

    database.Entry(updatedProduct).CurrentValues.SetValues(updatedProduct);
    await database.SaveChangesAsync();

    return Results.Ok($"Product {product.ProductId} updated successfully!");
});

//* DELETE:
app.MapDelete("/products/{id:int}", async (AppDbContext database, int id) =>
{
    var product = await database.Products.FindAsync(id);

    if(product is null) return Results.NotFound();

    database.Products.Remove(product);
    await database.SaveChangesAsync();

    return Results.NoContent();
});

//? Configuring the HTTP request pipeline...
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI((options) =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CatalogApi");
        options.RoutePrefix = string.Empty;
    });
}

//* Middlewares...
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();