using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Business;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly HistoryService _service;
        public HistoryController(HistoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(string buyerUsername, string sellerUsername, int page=1)
        {
            var vm = _service.GetIndexViewModel(sellerUsername, buyerUsername, page, CONSTANT.PAGESIZE);
            return View(vm);
        }
        [HttpPost]
        public IActionResult GetAll(IFormCollection collection)
        {
            return Json(collection);
        }
    }
}
