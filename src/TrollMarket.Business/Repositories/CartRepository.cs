using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class CartRepository:ICartRepository
    {
        private readonly TrollMarketContext _context;
        public CartRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public void AddToCart(Cart cart)
        {
            if(!_context.Products.Any(p=>p.Id==cart.ProductId && p.DiscontinueDate==null))
            {
                throw new InvalidOperationException("can't add to cart since product is not exist or discontinued");
            }
            if(!_context.Shipments.Any(s=> s.Id==cart.ShipmentId && s.ServiceStopDate == null))
            {
                throw new InvalidOperationException("cannot add to cart since shipment service not exist or has stopped it service");
            }
            if(!_context.AccountRoles.Any(a=> a.Username==cart.Buyer && a.Role == RoleEnum.Buyer))
            {
                throw new InvalidOperationException("cannot add to cart since account not exist or is not a buyer account");
            }
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }
        public bool CheckCartAlreadyExist(Cart cart)
        {
            return _context.Carts.Any(c=> c.ProductId == cart.ProductId && c.ShipmentId==cart.ShipmentId && c.Buyer==cart.Buyer);
        }
        public void UpdateCartQuantity(Cart cart)
        {
            var currentCart = GetCart(cart.Buyer, cart.ProductId, cart.ShipmentId);
            currentCart.Quantity += cart.Quantity;
            _context.Carts.Update(currentCart);
            _context.SaveChanges();
        }
        public Cart GetCart(string Buyer, int ProductId, int ShipmentId)
        {
            return _context.Carts
                    .Include(c=>c.BuyerNavigation)
                    .Include(c=>c.Shipment)
                    .Include(c=>c.Product)
                    .FirstOrDefault(c => c.ProductId == ProductId && c.ShipmentId == ShipmentId && c.Buyer == Buyer)
                ?? throw new KeyNotFoundException("Cart not found");
        }
        public List<Cart> GetAllCartByBuyer(string username)
        {
            return _context.Carts
                    .Include(c=>c.Shipment)
                    .Include(c=>c.Product)
                    .ThenInclude(p=>p.SellerNavigation)
                    .Where(c=>c.Buyer == username).ToList();
        }
        public PaginationResponse<Cart> GetCartPaginationResponse(string Buyer, int currentPage, int pageSize)
        {
            var data = from cart in _context.Carts
                    .Include(c => c.BuyerNavigation)
                    .Include(c => c.Shipment)
                    .Include(c => c.Product)
                    .ThenInclude(c=>c.SellerNavigation)
                       where cart.Buyer == Buyer
                       select cart;
            var totalItem = data.Count();
            var retrieveData = data.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
            return new PaginationResponse<Cart>
            {
                Data = retrieveData,
                CurrentPage= currentPage,
                TotalItems= totalItem,
                PageSize=pageSize
            };

        }
        public decimal GetCartTotalPrice(string buyer)
        {
            return _context.Carts
                    .Include(c=>c.Product)
                    .Include(c=>c.Shipment)
                    .Where(c=>c.Buyer==buyer)
                    .Sum(c=>c.Quantity*c.Product.Price+c.Shipment.Price);
        }
        public void RemoveCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
