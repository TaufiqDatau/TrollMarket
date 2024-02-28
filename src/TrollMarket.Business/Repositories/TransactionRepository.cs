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
    public class TransactionRepository:ITransactionRepository
    {
        private readonly TrollMarketContext _context;
        public TransactionRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public void TopUpTransaction(string username, decimal amount)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Accounts.FirstOrDefault(u=>u.Username==username);
                    
                    decimal currentBalance = user!.Balance;
                    decimal transactionValue = amount;
                    decimal finalBalance = currentBalance + transactionValue;

                    user.Balance = finalBalance;

                    var newLedgerEntry = new Ledger
                    {
                        Username = username,
                        Type = TransactionTypeEnum.Top_Up,
                        Timestamps = DateTime.Now,
                        CurrentBalance = currentBalance,
                        TransactionValue = transactionValue,
                        FinalBalance = finalBalance
                    };

                    
                    _context.Ledgers.Add(newLedgerEntry);
                    _context.Accounts.Update(user);

                    _context.SaveChanges();

                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback();
                    throw new ApplicationException("Error occurred while top up balance.", ex);
                }
            }


        }
        public void PurchaseTransaction(Account buyer,List<Cart> purchaseProduct)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var currentBalance = _context.Accounts.FirstOrDefault(a => a.Username == buyer.Username)?.Balance;
                    if(currentBalance == null)
                    {
                        throw new InvalidOperationException("The username is not registered in the system");
                    }
                    foreach (var cart in purchaseProduct)
                    {
                        var sellerUsername = cart.Product.Seller;
                        var seller = _context.Accounts.FirstOrDefault(a => a.Username == sellerUsername);
                        var buyerLedger = new Ledger
                        {
                            CurrentBalance = currentBalance,
                            Timestamps= DateTime.Now,
                            Username = buyer.Username,
                            Type = TransactionTypeEnum.Purchased
                        };
                        var sellerLedger = new Ledger
                        {
                            CurrentBalance = _context.Accounts.FirstOrDefault(a=>a.Username==sellerUsername).Balance,
                            Timestamps= DateTime.Now,
                            Username = sellerUsername,
                            Type = TransactionTypeEnum.Sell
                        };

                        decimal totalProductPrice = cart.Quantity * cart.Product.Price;
                        decimal totalPrice = totalProductPrice + cart.Shipment.Price;
                        currentBalance -= totalPrice;

                        if(currentBalance < 0)
                        {
                            throw new InvalidOperationException("Saldo anda tidak cukup untuk melakukan transaksi ini");
                        }

                        buyerLedger.TransactionValue = -totalPrice;
                        buyerLedger.FinalBalance = buyerLedger.CurrentBalance 
                                                    + buyerLedger.TransactionValue;
                        
                        buyer.Balance =(decimal) buyerLedger.FinalBalance;

                        sellerLedger.TransactionValue = totalProductPrice;
                        sellerLedger.FinalBalance=sellerLedger.CurrentBalance
                                                    + sellerLedger.TransactionValue;

                        seller.Balance=(decimal) sellerLedger.FinalBalance;
                        
                        _context.Accounts.Update(buyer);
                        _context.Accounts.Update(seller);
                        _context.Ledgers.Add(buyerLedger);
                        _context.Ledgers.Add(sellerLedger);

                        var orderData = new Order
                        {
                            Buyer=buyer.Username,
                            ProductId=cart.ProductId,
                            ShipmentId=cart.ShipmentId,
                            UnitPrice=cart.Product.Price,
                            ShipmentPrice=cart.Shipment.Price,
                            Quantity=cart.Quantity,
                            TransactionDate=DateTime.Now
                        };
                        _context.Carts.Remove(cart);
                        _context.Orders.Add(orderData);

                        _context.Accounts.Update(buyer);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }catch(InvalidOperationException e)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException(e.Message);
                }catch(Exception e)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Error occured while Purchasing product", e);
                }
            }
        }
        public Ledger GetLastLedgerTransaction(string username)
        {
            var userLedger = _context.Ledgers
                        .Where(l => l.Username == username)
                        .OrderByDescending(l => l.Timestamps)
                        .FirstOrDefault();
            return userLedger;
        }
    }
}
