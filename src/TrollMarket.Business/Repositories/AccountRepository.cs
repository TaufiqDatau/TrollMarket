using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess;
using TrollMarket.DataAccess.Models;
namespace TrollMarket.Business.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly TrollMarketContext _context;
        public AccountRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public List<AccountRole> GetAllAccountByRole(RoleEnum role) {
            return _context.AccountRoles.Include(a=>a.UsernameNavigation).Where(a=>a.Role == role).ToList();
        }
        public Account GetAccount(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username)
                            ?? throw new KeyNotFoundException("The account is not registered");

        }

        public bool IsAccountRoleMatch(string username, RoleEnum role)
        {
            return _context.AccountRoles.Any(a=>a.Username.Equals(username) && a.Role == role);
        }
        
        public void AddNewAccount(Account newAccount, RoleEnum role)
        {
            try
            {
                _context.Accounts.Add(newAccount);
                _context.AccountRoles.Add(new AccountRole { Username = newAccount.Username, Role = role });
                _context.SaveChanges();
            }catch(DbUpdateException e)
            {
                throw e.InnerException;
            }catch(Exception e)
            {
                throw new ApplicationException("Error adding a new account. Please try again later.", e);
            }
        }

        public void AddNewRoleToAccount(AccountRole newAccountRole)
        {
            try
            {
                _context.AccountRoles.Add(newAccountRole);
                _context.SaveChanges();
            }catch(DbException dbe)
            {
                throw new ApplicationException("Error when adding a new role.",dbe);
            }
            
        }
        

    }
}
