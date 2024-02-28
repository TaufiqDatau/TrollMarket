using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Shipment
{
    public class ShipmentIndexViewModel
    {
        public List<ShipmentTableViewModel> ShipmentTable { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
