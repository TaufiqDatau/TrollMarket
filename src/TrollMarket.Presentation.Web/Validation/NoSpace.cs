using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Presentation.Web.Validation
{
    public class NoSpace:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Username = (string)value;
            if (Username == null)
            {
                return new ValidationResult("Username must be filled");
            }
            if (Username.Contains(" "))
            {
                return new ValidationResult("Username cannot contain spaces");
            }

            return ValidationResult.Success;
        }
    }
}
