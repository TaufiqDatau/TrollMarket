using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.ViewModel.History;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class HistoryService
    {
        private readonly IOrderRepository _repository;
        private readonly IAccountRepository _accountRepository;
        public HistoryService(IOrderRepository repository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }

        public HistoryIndexViewModel GetIndexViewModel(string seller, string buyer, int currentPage, int pageSize)
        {
            var paginationResponse = _repository.GetHistoryPaginationResponse(seller, buyer, currentPage, pageSize);
            var historyTable = paginationResponse.Data.Select(order => new HistoryTableViewModel
            {
                Date = order.TransactionDate.ToString("dd/MM/yyyy"),
                BuyerName = order.BuyerNavigation.Name,
                SellerName = order.Product.SellerNavigation.Name,
                ProductName = order.Product.Name,
                ShipmentName = order.Shipment.Name,
                Quantity = order.Quantity,
                TotalPrice = ((order.UnitPrice * order.Quantity) + order.ShipmentPrice).ToString("c")

            });
            return new HistoryIndexViewModel
            {
                SellerUsername = seller,
                BuyerUsername = buyer,
                HistoryTables = historyTable.ToList(),
                PaginationInfo = Converter.ConvertToViewModel(paginationResponse),
                SellerList = _accountRepository.GetAllAccountByRole(RoleEnum.Seller).Select(a => new SelectListItem
                {
                    Value = a.Username,
                    Text = a.UsernameNavigation.Name,
                }).ToList(),
                BuyerList = _accountRepository.GetAllAccountByRole(RoleEnum.Buyer).Select(a => new SelectListItem
                {
                    Value = a.Username,
                    Text = a.UsernameNavigation.Name,
                }).ToList()
            };
        }
    }
}
