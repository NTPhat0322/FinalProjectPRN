
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;
using System.Collections;

namespace BLL.Services
{
    public class StaffInfoService
    {
        private StaffInfoRepository _staffInfoRepository;
        private AccountRepository _accountRepository;
        private DepartmentRepository _departmentRepository;
        public StaffInfoService()
        {
            _staffInfoRepository = new StaffInfoRepository();
            _accountRepository = new AccountRepository();
            _departmentRepository = new DepartmentRepository();
        }

        public List<StaffInfo> GetAll()
        {
            return _staffInfoRepository.GetAll();
        }

        public List<StaffInformationDTO> CreateStaffInfoDTO()
        {
            var staffInfos = _staffInfoRepository.GetAll();
            List<StaffInformationDTO> rs = new();

            foreach(var staffInfo in staffInfos)
            {
                var account = _accountRepository.GetById(staffInfo.AccountId);
                var department = _departmentRepository.GetById(staffInfo.DepartmentId);

                if (account != null && department != null)
                {
                    StaffInformationDTO staffInfoDTO = new()
                    {
                        Id = staffInfo.Id,
                        StaffName = account.Name,
                        AccountId = account.Id,
                        DepartmentId = department.Id,
                        DepartmentName = department.Name,
                        Degree = staffInfo.Degree,
                        YearOfExperience = staffInfo.YearOfExperience
                    };
                    rs.Add(staffInfoDTO);
                }
            }
            return rs;
        }

        public List<StaffInfo> GetAllStaff()
        {
            return _staffInfoRepository.GetAllStaff();
        }
    }
}
