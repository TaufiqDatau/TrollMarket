using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface ITransactionRepository
    {
        public void TopUpTransaction(string username, decimal amount);
        public void PurchaseTransaction(Account buyer, List<Cart> purchaseProduct);
        public Ledger GetLastLedgerTransaction(string username);
    }
}
