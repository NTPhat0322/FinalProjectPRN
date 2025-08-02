using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ScheduleRepository
    {
        private readonly GenderCareContext _context;

        public ScheduleRepository()
        {
            _context = new GenderCareContext();
        }

        public List<Schedule> GetByAccount(int accountId)
        {
            return _context.Schedules
                .Include(s => s.Service)
                .Include(s => s.Account)
                .Where(s => s.AccountId == accountId && !s.IsDeleted)
                .ToList();
        }

        public bool Add(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int scheduleId)
        {
            var schedule = _context.Schedules.FirstOrDefault(s => s.Id == scheduleId);
            if (schedule == null) return false;

            schedule.IsDeleted = true;
            _context.Schedules.Update(schedule);
            return _context.SaveChanges() > 0;
        }

        public List<Schedule> GetSchedulesByDoctor(int doctorId)
        {
            return _context.Schedules
                           .Include(s => s.Account)   
                           .Include(s => s.Service)
                           .Where(s => s.DoctorId == doctorId && !s.IsDeleted)
                           .OrderBy(s => s.ScheduleTime)
                           .ToList();
        }
    }
}
