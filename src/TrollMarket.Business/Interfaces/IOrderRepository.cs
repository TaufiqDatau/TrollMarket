using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;
using TrollMarket.DataAccess;

namespace TrollMarket.Business.Interfaces
{
    public interface IOrderRepository
    {
        public PaginationResponse<Order> GetTransactionPaginationResponse(string username, RoleEnum role, int currentPage, int pageSize);
        public PaginationResponse<Order> GetHistoryPaginationResponse(string seller, string buyer, int currentPage, int pageSize);
    }
}
