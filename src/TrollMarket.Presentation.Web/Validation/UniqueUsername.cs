using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Presentation.Web.Validation
{
    public class UniqueUsername:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Username = (string)value;
            var dbContext = (TrollMarketContext)validationContext.GetService(typeof(TrollMarketContext));
            if (Username == null)
            {
                return new ValidationResult("Username must be filled");
            }
            if (dbContext.Accounts.Any(a => a.Username == Username))
            {
                return new ValidationResult("Username already taken");
            }

            return ValidationResult.Success;
        }
    }
}
