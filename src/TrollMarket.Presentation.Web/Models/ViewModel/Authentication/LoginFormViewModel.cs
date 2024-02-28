using TrollMarket.DataAccess;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Authentication
{
    public class LoginFormViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
