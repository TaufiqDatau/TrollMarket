namespace TrollMarket.Presentation.Web.Models.ViewModel.MyCart
{
    public class CartTableViewModel
    {
        public int ProductId { get; set; }
        public int ShipmentId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ShipmentName { get; set; }
        public string SellerName { get; set; }
        public string TotalPrice { get; set; }
    }
}
