using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.Identity.Client;

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

        public Account? GetByEmail(string email)
        {
            return _accountRepo.GetByEmail(email);
        }

        public List<Account> GetAll()
        {
            return _accountRepo.GetAll();
        }
        public bool Add(Account account)
        {
            return _accountRepo.Add(account);
        }
        public Account? GetById(int id)
        {
            return _accountRepo.GetById(id);
        }
        public bool Update(Account account)
        {
            return _accountRepo.Update(account);
        }
        public bool Delete(Account account)
        {
            return _accountRepo.Delete(account);
        }

    }
}
