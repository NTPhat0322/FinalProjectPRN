
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
    }
}
