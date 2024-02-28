using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.Validation;
namespace TrollMarket.Presentation.Web.Models.ViewModel.Authentication
{
    public class RegisterUserViewModel
    {
        [UniqueUsername]
        [NoSpace]
        public string Username { get; set; }
        [Compare("ConfirmPassword", ErrorMessage = "Password is not same")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password is not same")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        public RoleEnum Role { get; set; }
    }
}
