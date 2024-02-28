using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.Models.DTO;

namespace TrollMarket.Presentation.Web.Services.API
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly ICartRepository _cartRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionService(ITransactionRepository repository, ICartRepository cartRepository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _accountRepository = accountRepository;
        }
        public void TopUpTransaction(TransferRequestDTO dto)
        {
            _repository.TopUpTransaction(dto.Username, dto.Amount);
        }
        public void PurchaseAll(string username)
        {
            var buyerData = _accountRepository.GetAccount(username);
            var CartList = _cartRepository.GetAllCartByBuyer(username);
            _repository.PurchaseTransaction(buyerData, CartList);
        }
    }
}
