using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class AccountRepository
    {
        private GenderCareContext _context;

        public AccountRepository()
        {
            _context = new GenderCareContext();
        }

        public Account? GetByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(a => a.Email == email);
        }


        public List<Account> GetAll()
        {

            return _context.Accounts
                .ToList();
        }

        public bool Add(Account account)
        {

            _context.Accounts.Add(account);
            return _context.SaveChanges() > 0;
        }

        public Account? GetById(int id)
        {

            return _context.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(Account account)
        {
            Account? tmp = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if (tmp is null) return false;
            tmp.RoleId = account.RoleId;
            tmp.Name = account.Name;
            tmp.Email = account.Email;
            tmp.Password = account.Password;
            tmp.DateOfBirth = account.DateOfBirth;
            tmp.Gender = account.Gender;
            tmp.IsDeleted = account.IsDeleted;
            _context.Accounts.Update(tmp);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Account account)
        {
            Account? tmp = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if (tmp is null) return false;
            _context.Accounts.Remove(tmp);
            _context.SaveChanges();
            return true;
        }

    }
}
