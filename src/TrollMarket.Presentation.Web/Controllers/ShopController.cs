using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Business;
using TrollMarket.Presentation.Web.Models.ViewModel.Shop;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles ="Buyer")]
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly ProductService _service;
        public ShopController(ProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(string name="", string category="", string description="",int page=1)
        {
            if (string.IsNullOrEmpty(name))
            {
                name="";
            }

            if (string.IsNullOrEmpty(category))
            {
                category="";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "";
            }

            var vm = _service.GetShopIndexViewModel(name,category,description,page,CONSTANT.PAGESIZE);
            return View(vm);
        }
    }
}
