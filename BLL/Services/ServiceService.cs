
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
    }
}
