using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Shop
{
    public class ShopIndexViewModel
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public List<ShopTableViewModel> ShopTable { get; set; }=new List<ShopTableViewModel>();
        public List<SelectListItem> SelectShipment { get; set; } =new List<SelectListItem>();
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
