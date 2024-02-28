using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Web.Validation;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Authentication
{
    public class RegisterAdminViewModel
    {
        [UniqueUsername]
        [NoSpace]
        public string Username { get; set; }
        [Compare("ConfirmPassword", ErrorMessage = "Password is not same")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password is not same")]
        public string ConfirmPassword { get; set; }
    }
}
