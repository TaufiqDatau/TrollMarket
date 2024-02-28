using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface ICartRepository
    {
        public void AddToCart(Cart cart);
        public List<Cart> GetAllCartByBuyer(string username);
        public bool CheckCartAlreadyExist(Cart cart);
        public void UpdateCartQuantity(Cart cart);
        public Cart GetCart(string Buyer, int ProductId, int ShipmentId);
        public decimal GetCartTotalPrice(string buyer);
        public PaginationResponse<Cart> GetCartPaginationResponse(string Buyer, int currentPage, int pageSize);
        public void RemoveCart(Cart cart);
    }
}
