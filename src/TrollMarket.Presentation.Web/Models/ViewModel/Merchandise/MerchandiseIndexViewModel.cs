using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Merchandise
{
    public class MerchandiseIndexViewModel
    {
        public List<MerchandiseTableViewModel> Merchandises { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
