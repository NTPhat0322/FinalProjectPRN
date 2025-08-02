using DAL.Entities;
using DAL.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StaffInfoRepository
    {
        private GenderCareContext _context;
        public StaffInfoRepository()
        {
            _context = new GenderCareContext();
        }
        public List<StaffInfo> GetAll()
        {

            return _context.StaffInfos
                .ToList();
        }

        public void Add(StaffInfo staffInfo)
        {

            _context.StaffInfos.Add(staffInfo);
            _context.SaveChanges();
        }
        public StaffInfo? GetById(int id)
        {
            return _context.StaffInfos.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(StaffInfo staffInfo)
        {
            StaffInfo? tmp = _context.StaffInfos.FirstOrDefault(a => a.Id == staffInfo.Id);
            if (tmp is null) return false;
            tmp.AccountId = staffInfo.AccountId;
            tmp.DepartmentId = staffInfo.DepartmentId;
            tmp.Degree = staffInfo.Degree;
            tmp.YearOfExperience = staffInfo.YearOfExperience;
            _context.StaffInfos.Update(tmp);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(StaffInfo staffInfo)
        {
            StaffInfo? tmp = _context.StaffInfos.FirstOrDefault(a => a.Id == staffInfo.Id);
            if (tmp is null) return false;
            _context.StaffInfos.Remove(tmp);
            _context.SaveChanges();
            return true;
        }

        public List<StaffInfo> GetAllStaff()
        {
            return _context.StaffInfos
                           .Include(s => s.Account)
                               .ThenInclude(a => a.Role)
                           .Include(s => s.Department)
                           .Where(s => s.Account.Role.Name == RoleName.Doctor && !s.Account.IsDeleted)
                           .ToList();
        }
    }
}
