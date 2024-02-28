using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Services.API;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;
        public TransactionController(TransactionService service)
        {
            _service = service;
        }
        [HttpPatch("topup")]
        public IActionResult TopUp(TransferRequestDTO requestDTO)
        {
            try
            {
                _service.TopUpTransaction(requestDTO);
                return Ok();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }
        }
        [HttpPost("{username}/purchase")]
        public IActionResult PurchaseAll(string username)
        {
            try
            {
                _service.PurchaseAll(username);
                return Ok();
            }catch(InvalidOperationException ex)
            {
                var errorMessage = ex.Message;
                return BadRequest(new {errorMessage });
            }catch(Exception ex)
            {
                var errorMessage = ex.Message;
                return StatusCode(500, new { errorMessage });
            }
        }

    }
}
