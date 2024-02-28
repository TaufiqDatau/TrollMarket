namespace TrollMarket.Presentation.Web.HelperClass
{
    public class PaginationInfoViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItem {  get; set; }
        public int PageSize { get; set; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItem / PageSize);
            }
        }
    }
}
