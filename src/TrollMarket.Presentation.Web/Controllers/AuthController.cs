using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.Services.MVC;
using TrollMarket.Presentation.Web.Models.ViewModel.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("")]
    public class AuthController : Controller
    {
        private readonly AuthService _service;
        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginFormViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var ticket = _service.Login(vm);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                            ticket.Principal, ticket.Properties);
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(vm);
            }
        }
        [HttpGet("register/{role}")]
        public IActionResult Register(RoleEnum role)
        {
            var vm = new RegisterUserViewModel { Role = role };
            if (role == RoleEnum.Admin)
            {
                TempData["ErrorMessage"] = "Register as admin is not allowed";
                return RedirectToAction("login");
            }
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View(vm);
        }

        [HttpPost("register/{role}")]
        public IActionResult Register(RegisterUserViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                _service.Register(vm);
                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(vm);
            }
        }

        [HttpGet("addrole")]
        public IActionResult AddRole(RoleEnum role)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            if (role == RoleEnum.Admin)
            {
                TempData["ErrorMessage"] = "Register as admin is not allowed";
                return RedirectToAction("login");
            } 
            var vm = new AddRoleFormViewModel { Role = role };
            return View(vm);
        }
        [HttpPost("addRole")]
        public IActionResult AddRole(AddRoleFormViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                _service.AddNewRoleToAccount(vm);
                TempData["SuccessMessage"] = "New Role has been added to your account! You can now log in.";
                return RedirectToAction("login");
            }catch(Exception ex)
            {
                TempData["ErrorMessage"] =ex.Message;
                return RedirectToAction("login");
            }
            
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("Admin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost("Admin")]
        public IActionResult RegisterAdmin(RegisterAdminViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            _service.Register(vm);
            ViewBag.SuccessMessage = "Admin succesfully added to the system! you can now log in";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        [HttpGet("Error")]
        public IActionResult Error()
        {
            return View();
        }

    }
}
