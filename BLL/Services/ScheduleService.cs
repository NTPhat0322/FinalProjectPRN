using DAL.Entities;
using DAL.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ScheduleService
    {
        private readonly ScheduleRepository _repo;

        public ScheduleService()
        {
            _repo = new ScheduleRepository();
        }

        public List<Schedule> GetByAccount(int accountId)
        {
            return _repo.GetByAccount(accountId);
        }

        public bool Add(Schedule schedule)
        {
            return _repo.Add(schedule);
        }

        public bool Delete(int scheduleId)
        {
            return _repo.Delete(scheduleId);
        }

        public List<Schedule> GetByDoctor(int doctorId)
        {
            return _repo.GetSchedulesByDoctor(doctorId);
        }

    }
}
