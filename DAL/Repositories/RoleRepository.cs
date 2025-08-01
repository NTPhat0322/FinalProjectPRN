using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RoleRepository
    {
        private GenderCareContext _context;
        public RoleRepository()
        {
            _context = new GenderCareContext();
        }
        public List<Role> GetAll()
        {

            return _context.Roles
                .ToList();
        }

        public void Add(Role role)
        {

            _context.Roles.Add(role);
            _context.SaveChanges();
        }
        public Role? GetById(int id)
        {
            return _context.Roles.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(Role role)
        {
            Role? tmp = _context.Roles.FirstOrDefault(a => a.Id == role.Id);
            if (tmp is null) return false;
            tmp.Name = role.Name;
            tmp.IsDeleted = role.IsDeleted;
            _context.Roles.Update(tmp);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Role role)
        {
            Role? tmp = _context.Roles.FirstOrDefault(a => a.Id == role.Id);
            if (tmp is null) return false;
            tmp.IsDeleted = true; // Soft delete
            _context.Roles.Update(tmp);
            _context.SaveChanges();
            return true;
        }
    }
}
