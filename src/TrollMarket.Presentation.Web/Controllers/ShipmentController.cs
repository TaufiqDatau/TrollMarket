using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Business;
using TrollMarket.Presentation.Web.Models.ViewModel.Shipment;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]")]
    public class ShipmentController : Controller
    {
        private readonly ShipmentService _service;
        public ShipmentController(ShipmentService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            var vm = _service.GetShipmentIndexViewModel(page, CONSTANT.PAGESIZE);
            return View(vm);
        }
    }
}
