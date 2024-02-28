using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.ViewModel;
using TrollMarket.Presentation.Web.Models.ViewModel.Profile;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class ProfileService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;
        public ProfileService(IOrderRepository repository,IAccountRepository accountRepository)
        {
            _orderRepository = repository;
            _accountRepository = accountRepository;
        }
        public ProfileIndexViewModel GetProfileIndexViewModel(string username, RoleEnum role, int currentPage, int pageSize)
        {
            var paginationResponse = _orderRepository.GetTransactionPaginationResponse(username, role, currentPage, pageSize);
            var userData = _accountRepository.GetAccount(username);
            var vm = new ProfileIndexViewModel
            {
                Name = userData.Name,
                Role = role.ToString(),
                Address = userData.Address,
                Balance = userData.Balance.ToString("c"),

                TransactionList = (from transaction in paginationResponse.Data
                                   select new TransactionTableViewModel
                                   {
                                       Date = transaction.TransactionDate.ToString("dd/MM/yyyy"),
                                       Product= transaction.Product.Name,
                                       Shipment=transaction.Shipment.Name,
                                       Quantity=transaction.Quantity,
                                       TotalPrice=(transaction.Quantity*transaction.UnitPrice + transaction.ShipmentPrice).ToString("c")

                                   }).ToList(),
                paginationInfo = Converter.ConvertToViewModel(paginationResponse)
            };
            return vm;
        }
    }
}
