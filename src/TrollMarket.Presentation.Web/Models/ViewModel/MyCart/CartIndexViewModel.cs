using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.MyCart
{
    public class CartIndexViewModel
    {
        
        public decimal CurrentBalance { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartTableViewModel> CartTable { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
