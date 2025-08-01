
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class ServiceService
    {
        private ServiceRepository _serviceRepository;
        public ServiceService()
        {
            _serviceRepository = new ServiceRepository();
        }
        public List<Service> GetAll()
        {
            return _serviceRepository.GetAll();
        }
        // Thêm dịch vụ mới
        public void Add(Service service)
        {
            _serviceRepository.Add(service);
        }
        // Cập nhật dịch vụ
        public bool Update(Service service)
        {
            return _serviceRepository.Update(service);
        }

        // Xóa dịch vụ
        public bool Delete(Service service)
        {
            return _serviceRepository.Delete(service);
        }
    }
}
