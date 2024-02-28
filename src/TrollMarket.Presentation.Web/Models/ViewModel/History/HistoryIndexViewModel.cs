using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.History
{
    public class HistoryIndexViewModel
    {
        public string BuyerUsername { get; set; }
        public string SellerUsername { get; set;}
        public List<HistoryTableViewModel> HistoryTables { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
        public List<SelectListItem>? BuyerList { get; set; }
        public List<SelectListItem>? SellerList { get; set; }
    }
}
