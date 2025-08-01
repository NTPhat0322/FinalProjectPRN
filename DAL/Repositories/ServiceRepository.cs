using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ServiceRepository
    {
        private GenderCareContext _context;
        public ServiceRepository()
        {
            _context = new GenderCareContext();
        }
        public List<Service> GetAll()
        {

            return _context.Services
                .ToList();
        }

        public void Add(Service service)
        {

            _context.Services.Add(service);
            _context.SaveChanges();
        }
        public Service? GetById(int id)
        {
            return _context.Services.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(Service service)
        {
            Service? tmp = _context.Services.FirstOrDefault(a => a.Id == service.Id);
            if (tmp is null) return false;
            tmp.Name = service.Name;
            tmp.Description = service.Description;
            tmp.Price = service.Price;
            tmp.IsDeleted = service.IsDeleted;
            _context.Services.Update(tmp);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Service service)
        {
            Service? tmp = _context.Services.FirstOrDefault(a => a.Id == service.Id);
            if (tmp is null) return false;
            tmp.IsDeleted = true;
            _context.Services.Update(tmp);
            _context.SaveChanges();
            return true;
        }
    }
}
