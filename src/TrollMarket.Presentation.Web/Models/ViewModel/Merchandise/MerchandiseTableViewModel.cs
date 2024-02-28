namespace TrollMarket.Presentation.Web.Models.ViewModel.Merchandise
{
    public class MerchandiseTableViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category {  get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public string DiscontinueStatus
        {
            get
            {
                if (DiscontinuedDate.HasValue)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }
    }
}
