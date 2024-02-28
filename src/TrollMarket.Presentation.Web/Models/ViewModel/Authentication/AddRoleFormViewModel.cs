using TrollMarket.DataAccess;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Authentication
{
    public class AddRoleFormViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public RoleEnum Role { get; set; }
    }
}
