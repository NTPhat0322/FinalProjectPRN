
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class DepartmentService
    {
        private DepartmentRepository _departmentRepository;
        public DepartmentService()
        {
            _departmentRepository = new DepartmentRepository();
        }
        public List<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }
        public void Add(Department department)
        {
            _departmentRepository.Add(department);
        }
        //update
        public bool Update(Department department)
        {
            return _departmentRepository.Update(department);
        }
        //delete
        public bool Delete(Department department)
        {
            return _departmentRepository.Delete(department);
        }
    }
}
