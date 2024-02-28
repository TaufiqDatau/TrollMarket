using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Validation
{
    public class MaxNumericValue : ValidationAttribute
    {
        private readonly long maxValue;

        public MaxNumericValue(long maxValue)
        {
            this.maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are considered valid, handle as needed
            }

            if (!long.TryParse(value.ToString(), out long intValue))
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a valid integer.");
            }

            if (Math.Abs(intValue) > maxValue)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be less than or equal to {maxValue}.");
            }

            return ValidationResult.Success;
        }
    }
}
