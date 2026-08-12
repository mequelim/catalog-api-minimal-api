using System.ComponentModel.DataAnnotations;

namespace CatalogApiMinimalApi.Utils.Validations;

public class FirstCapitalLetterAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value switch
        {
            string str when(!string.IsNullOrEmpty(str)) => char.IsUpper(str[0])
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} must start with a capital letter!"),

            char ch => char.IsUpper(ch)
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} must be a capital letter!"),

            _ => ValidationResult.Success
        };
    }
}