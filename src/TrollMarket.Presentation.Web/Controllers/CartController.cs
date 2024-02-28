using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Business;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles ="Buyer")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Index(int page=1)
        {
            var username = User.FindFirst("username").Value;
            var vm = _cartService.GetCartIndexViewModel(username, page, CONSTANT.PAGESIZE);
            return View(vm);
        }
    }
}
