using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class RoleService
    {
        private RoleRepository _roleRepo;
        public RoleService()
        {
            _roleRepo = new RoleRepository();
        }
        public List<Role> GetAllRoles()
        {
            return _roleRepo.GetAll();
        }
        public Role? GetRoleById(int id)
        {
            return _roleRepo.GetById(id);
        }
        public void AddRole(Role role)
        {
            _roleRepo.Add(role);
        }
        public bool UpdateRole(Role role)
        {
            return _roleRepo.Update(role);
        }
        public bool DeleteRole(Role role)
        {
            return _roleRepo.Delete(role);
        }
    }
}
