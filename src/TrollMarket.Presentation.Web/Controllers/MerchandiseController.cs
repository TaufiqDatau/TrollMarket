using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Services.MVC;
using System.Security.Claims;
using TrollMarket.Business;
using TrollMarket.Presentation.Web.Models.ViewModel.Merchandise;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles ="Seller")]
    [Route("[controller]")]
    public class MerchandiseController : Controller
    {
        private readonly ProductService _service;
        public MerchandiseController(ProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var username = User.FindFirst("username").Value;
            var vm = _service.GetIndexViewModel(username, page, CONSTANT.PAGESIZE);

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            return View(vm);
        }
        [HttpGet("insert")]
        public IActionResult Insert()
        {
            var vm = new MerchandiseUpsertViewModel
            {
                Seller = User.FindFirst("username").Value
            };
            return View("upsert", vm);
        }
        [HttpPost("insert")]
        public IActionResult Insert(MerchandiseUpsertViewModel vm)
        {
            if(!ModelState.IsValid)
            {

                return View("upsert",vm);
            }
            _service.InsertNewProduct(vm);
            TempData["SuccessMessage"] = "New product is added";
            return RedirectToAction("index");
        }
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var vm = _service.GetProductById(id);
            if (_service.CheckDependencies(id))
            {
                TempData["ErrorMessage"] = "You cannot edit this item because it already a transaction for this product";
                return RedirectToAction("index");
            }
            if(vm.Discontinue)
            {
                TempData["ErrorMessage"] = "You cannot edit this item because it already discontinued";
                return RedirectToAction("index");
            }
            return View("upsert",vm);
        }
        [HttpPost("edit")]
        public IActionResult Edit(MerchandiseUpsertViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("upsert", vm);
            }
            _service.EditProduct(vm);
            TempData["SuccessMessage"] = "Edit Successfull";
            return RedirectToAction("index");
        }
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            if (_service.CheckDependencies(id))
            {
                TempData["ErrorMessage"] = "You can't delete this item because there already a transaction for this product";
                return RedirectToAction("index");
            }
            _service.DeleteProduct(id);
            TempData["SuccessMessage"] = "Item deleted";
            return RedirectToAction("index");
        }
    }
}
