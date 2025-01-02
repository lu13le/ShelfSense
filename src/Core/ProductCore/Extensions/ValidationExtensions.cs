using System.ComponentModel.DataAnnotations;

namespace ProductCore.Extensions;

public static class ValidationExtensions
{
    public static bool TryValidate<T>(this T? request, out string? errorMessage)
    {
        errorMessage = null;

        if (request is null)
        {
            errorMessage = "Request cannot be null.";
            return false;
        }

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        var isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
        errorMessage = isValid ? null : validationResults.FirstOrDefault()?.ErrorMessage;

        return isValid;
    }
}
