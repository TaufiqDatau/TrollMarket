namespace TrollMarket.Presentation.Web.Models.ViewModel.Merchandise
{
    public class MerchandiseUpsertViewModel
    {
        public int ProductId { get; set; }
        public string Seller {  get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public decimal Price {  get; set; }
        public bool Discontinue {  get; set; }
    }
}
