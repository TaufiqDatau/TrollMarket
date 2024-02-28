using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Services.API;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Route("api/shipment")]
    [ApiController]
    public class APIShipmentController : ControllerBase
    {
        private readonly APIShipmentService _service;
        public APIShipmentController(APIShipmentService service)
        {
            _service = service;
        }
        [HttpGet("{id}/GET")]
        public IActionResult GetShipment(int id)
        {
            try
            {
            var dto = _service.GetShipmentForEdit(id);
            return Ok(dto);

            }catch(Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest(new { errorMessage });
            }
        }
        [HttpPut]
        public IActionResult EditShipment(ShipmentDTO dto)
        {
            try
            {
                _service.EditShipment(dto);
                return Ok();

            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
                return BadRequest(new { errorMessage });
            }
        }
        [HttpPost]
        public IActionResult NewShipmentCompany(ShipmentDTO dto)
        {
            try
            {
                _service.InsertNewShipment(dto);
                return Created("http://localhost:5089/api/shipment", dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("{id}/stopService/PATCH")]
        public IActionResult StopService(int id)
        {
            try
            {
                _service.StopShipmentService(id);
                return Ok();
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
                return BadRequest(new { errorMessage });
            }
        }
        [HttpDelete("{id}/DELETE")]
        public IActionResult DeleteShipment(int id)
        {
            try
            {

                _service.DeleteShipment(id);
                return Ok();
            }catch(Exception ex)
            {
                var errorMessage=ex.Message;
                return BadRequest(new { errorMessage });
            }
        }

    }
}
