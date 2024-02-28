using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface IAccountRepository
    {
        public List<AccountRole> GetAllAccountByRole(RoleEnum role);
        public Account GetAccount(string username);
        public bool IsAccountRoleMatch(string username, RoleEnum role);
        public void AddNewAccount(Account newAccount, RoleEnum role);
        public void AddNewRoleToAccount(AccountRole newAccountRole);
    }
}
