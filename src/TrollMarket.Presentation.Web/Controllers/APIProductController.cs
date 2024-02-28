using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.Services.API;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class APIProductController : ControllerBase
    {
        private readonly APIProductService _service;

        public APIProductController(APIProductService service)
        {
            _service = service;
        }

        [HttpPatch("{id}/discontinue")]
        public IActionResult Discontinue(int id)
        {
            try
            {
                _service.DiscontinueProduct(id);
                return Ok();
            }
            catch(Exception ex) 
            {
                var errorMessage = ex.Message;
                return BadRequest(new {errorMessage});
            }
        }
        [HttpGet("{id}/infodetail")]
        public IActionResult GetInfoDetail(int id)
        {
            var dto=_service.GetProductDetail(id);
            return Ok(dto);
        }
    }
}
