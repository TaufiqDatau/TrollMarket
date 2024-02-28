using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Web.Validation;

namespace TrollMarket.Presentation.Web.Models.DTO
{
    public class CartDTO
    {
        public string? Buyer {  get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Please select a shipment")]
        public int ShipmentId { get; set; }
        [MaxNumericValue(1000000, ErrorMessage = "The value is too large.")]
        [Range(1, int.MaxValue, ErrorMessage ="Please fill the quantity with an appropriate value")]
        public int Quantity { get; set; }
    }
}
