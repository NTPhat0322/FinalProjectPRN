using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class AccountService
    {
        private AccountRepository _accountRepo;

        public AccountService()
        {
            _accountRepo = new AccountRepository();
        }

        public Account? Login(string email, string password)
        {
            var userAccount = _accountRepo.GetByEmail(email);
            if (userAccount == null)
            {
                return null; // User not found
            }
            // Check if the password matches
            if (userAccount.Password == password)
            {
                return userAccount; // Login successful
            }
            else
            {
                return null; // Incorrect password
            }
        }

    }
}
