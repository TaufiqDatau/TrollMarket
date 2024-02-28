namespace TrollMarket.Presentation.Web.Models.DTO
{
    public class ProductDetailDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public string? SellerName { get; set; }
        public bool Discontinue {  get; set; }
        public string DiscontinueStatus
        {
            get
            {
                if (Discontinue)
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
