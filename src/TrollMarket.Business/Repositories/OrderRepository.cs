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
    public class OrderRepository : IOrderRepository
    {
        private readonly TrollMarketContext _context;
        public OrderRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public PaginationResponse<Order> GetTransactionPaginationResponse(string username, RoleEnum role, int currentPage, int pageSize)
        {
            IQueryable<Order> query;
            if (role == RoleEnum.Seller)
            {
                query = from order in _context.Orders
                        .Include(o => o.Shipment)
                        .Include(o => o.Product)
                        .ThenInclude(p => p.SellerNavigation)
                        where order.Product.Seller == username
                        select order;
            }
            else
            {
                query = from order in _context.Orders
                        .Include(o => o.BuyerNavigation)
                        .Include(o => o.Shipment)
                        .Include(o => o.Product)
                        where order.Buyer == username
                        select order;
            }
            


            return new PaginationResponse<Order>
            {
                Data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                TotalItems = query.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize
            };
        }
        public PaginationResponse<Order> GetHistoryPaginationResponse(string seller, string buyer, int currentPage, int pageSize)
        {
            IQueryable<Order> query;



            query = from order in _context.Orders
                    .Include(o => o.BuyerNavigation)
                    .Include(o => o.Shipment)
                    .Include(o => o.Product)
                    .ThenInclude(p => p.SellerNavigation)
                    where (order.Product.Seller.Equals(seller)||String.IsNullOrEmpty(seller)) 
                        && (order.Buyer.Equals(buyer)||String.IsNullOrEmpty(buyer))
                    select order;

            return new PaginationResponse<Order>
            {
                Data = query.ToList(),
                TotalItems = query.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize
            };
        }
    }
}
