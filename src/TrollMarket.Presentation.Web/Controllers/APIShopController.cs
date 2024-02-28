using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Services.MVC;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class APIShopController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly CartService _cartService;
        public APIShopController(ProductService service, CartService cartService)
        {
            _service = service;
            _cartService = cartService;
        }
        [HttpPost("add")]
        public IActionResult AddCart(CartDTO dto)
        {
            var user = User as ClaimsPrincipal;
            var username = user.FindFirst("username").Value;
            var role = user.FindFirst(ClaimTypes.Role).Value;

            try
            {
                if (role != "Buyer")
                {
                    throw new InvalidOperationException($"you're logged in as {role} you're not allowed to do this operation");
                }
                
                _service.AddToCart(dto);
                return Created("http://localhost:5089/api/shop/add", dto);
            }catch(Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest(new {errorMessage});
            }
        }
        [HttpDelete]
        public IActionResult RemoveFromCart(CartDTO dto)
        {
            var user = User as ClaimsPrincipal;
            var role = user.FindFirst(ClaimTypes.Role).Value;
            _cartService.RemoveFromCart(dto);
            return NoContent();
        }
    }
}
