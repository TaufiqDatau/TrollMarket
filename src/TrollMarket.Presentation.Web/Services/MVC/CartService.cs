using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Models.ViewModel.MyCart;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class CartService
    {
        private readonly ICartRepository _repository;
        private readonly IAccountRepository _accountRepository;

        public CartService(ICartRepository repository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }
        public void RemoveFromCart(CartDTO dto)
        {
            var cart = _repository.GetCart(dto.Buyer, dto.ProductId, dto.ShipmentId);
            _repository.RemoveCart(cart);
        }
        public CartIndexViewModel GetCartIndexViewModel(string username, int currentPage, int pageSize)
        {
            var paginationResponse = _repository.GetCartPaginationResponse(username, currentPage, pageSize);
            var buyerData = _accountRepository.GetAccount(username);
            return new CartIndexViewModel
            {
                CartTable = paginationResponse.Data
                            .Select(c=>new CartTableViewModel
                            {
                                ProductId = c.ProductId,
                                ShipmentId=c.ShipmentId,
                                TotalPrice= CountTotalPricePerCart(c).ToString("c"),
                                SellerName=c.Product.SellerNavigation.Name,
                                ShipmentName=c.Shipment.Name,
                                ProductName=c.Product.Name,
                                Quantity=c.Quantity
                            }).ToList(),
                TotalPrice = _repository.GetCartTotalPrice(username),
                CurrentBalance = buyerData.Balance,
                PaginationInfo = Converter.ConvertToViewModel(paginationResponse)
            };
        }

        private decimal CountTotalPricePerCart(Cart cart)
        {
            return cart.Quantity * cart.Product.Price + cart.Shipment.Price;
        }
    }
}
