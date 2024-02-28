using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TrollMarket.Business;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Buyer, Seller")]
    public class ProfileController : Controller
    {
        private readonly ProfileService _service;
        public ProfileController(ProfileService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            var username = User?.FindFirst("username")?.Value;

            var role = User?.FindFirst(ClaimTypes.Role)?.Value;
            var userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), role);//Enum Conversion

            var vm = _service.GetProfileIndexViewModel(username, userRole,page,CONSTANT.PAGESIZE);
            return View(vm);
        }
    }
}
