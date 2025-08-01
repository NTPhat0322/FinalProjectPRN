using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class DepartmentRepository
    {
        private GenderCareContext _context;
        public DepartmentRepository()
        {
            _context = new GenderCareContext();
        }
        public List<Department> GetAll()
        {

            return _context.Departments
                .ToList();
        }

        public void Add(Department department)
        {

            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        public Department? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(Department department)
        {
            Department? tmp = _context.Departments.FirstOrDefault(a => a.Id == department.Id);
            if (tmp is null) return false;
            tmp.Name = department.Name;
            _context.Departments.Update(tmp);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Department department)
        {
            Department? tmp = _context.Departments.FirstOrDefault(a => a.Id == department.Id);
            if (tmp is null) return false;
            _context.Departments.Remove(tmp);
            _context.SaveChanges();
            return true;
        }
    }
}
