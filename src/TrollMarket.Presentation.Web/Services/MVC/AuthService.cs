using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models.ViewModel.Authentication;
using TrollMarket.DataAccess;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;
        public AuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AuthenticationTicket Login(LoginFormViewModel loginData)
        {
            
            var userData = _accountRepository.GetAccount(loginData.Username);

            if (!_accountRepository.IsAccountRoleMatch(loginData.Username, loginData.Role))
            {
                throw new InvalidOperationException($"You can't login as {loginData.Role.ToString()}");
            }

            VerifyPassword(loginData.Password, userData.Password);

            var claims = new List<Claim>{
                new Claim("username", userData.Username),
                new Claim(ClaimTypes.Role, loginData.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            };

            return new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public void Register(RegisterUserViewModel vm)
        {
            var newAccount = new Account
            {
                Username = vm.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                Name= vm.Name,
                Address= vm.Address,
            };
            _accountRepository.AddNewAccount(newAccount,vm.Role);
        }
        public void Register(RegisterAdminViewModel vm)
        {
            var newAdmin = new Account
            {
                Username = vm.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password)
            };
            _accountRepository.AddNewAccount(newAdmin, RoleEnum.Admin);
        }
        public void AddNewRoleToAccount(AddRoleFormViewModel vm)
        {
            var accountData = _accountRepository.GetAccount(vm.Username);

            VerifyPassword(vm.Password, accountData.Password);
            if (_accountRepository.IsAccountRoleMatch(vm.Username, vm.Role))
            {
                throw new InvalidOperationException($"This username already registered for {vm.Role.ToString()} role");
            }
            if(_accountRepository.IsAccountRoleMatch(vm.Username, RoleEnum.Admin))
            {
                throw new InvalidOperationException($"You can't add this username to {vm.Role.ToString()} role");
            }
            _accountRepository.AddNewRoleToAccount(new AccountRole { Username=vm.Username, Role=vm.Role });
        }
        private void VerifyPassword(string enteredPassword, string storedPassword)
        {
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);

            if (!isCorrectPassword)
            {
                throw new InvalidOperationException("Username or password is incorrect");
            }
        }
    }
}
