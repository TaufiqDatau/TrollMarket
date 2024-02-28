using TrollMarket.Presentation.Web.HelperClass;

namespace TrollMarket.Presentation.Web.Models.ViewModel.Profile
{
    public class ProfileIndexViewModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string Balance { get; set; }
        public List<TransactionTableViewModel> TransactionList { get; set; }
        public PaginationInfoViewModel paginationInfo { get; set; }
    }
}
