using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Models.DTO
{
    public class TransferRequestDTO
    {
        public string Username { get; set; }
        [Range(10000.00, double.MaxValue, ErrorMessage ="Amount must be more than Rp10000")]
        public decimal Amount {  get; set; }
    }
}
