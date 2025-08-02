
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

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

        public bool IsDepartmentInUse(int departmentId)
        {
            var staffInfos = _staffInfoRepository.GetAll();
            return staffInfos.Any(s => s.DepartmentId == departmentId);
        }
        //update
        public bool Update(StaffInformationDTO staffInfo)
        {
            StaffInfo? tmp = _staffInfoRepository.GetById(staffInfo.Id);
            if (tmp is null) return false;
            tmp.DepartmentId = staffInfo.DepartmentId;
            tmp.Degree = staffInfo.Degree;
            tmp.YearOfExperience = staffInfo.YearOfExperience;
            return _staffInfoRepository.Update(tmp);
        }
        //delete
        public bool Delete(int staffInfoId)
        {
            StaffInfo? staffInfo = _staffInfoRepository.GetById(staffInfoId);
            if (staffInfo is null) return false;
            return _staffInfoRepository.Delete(staffInfo);
        }
        public bool DeleteByAccountId(int accountId)
        {
            var staffInfos = _staffInfoRepository.GetAll();
            var staffInfo = staffInfos.FirstOrDefault(s => s.AccountId == accountId);
            if (staffInfo is null) return false;
            return _staffInfoRepository.Delete(staffInfo);
        }
        //add
        public bool Add(StaffInfo staffInfo)
        {
            if (staffInfo is null) return false;
            if (_accountRepository.GetById(staffInfo.AccountId) is null) return false;
            if (_departmentRepository.GetById(staffInfo.DepartmentId) is null) return false;
            _staffInfoRepository.Add(staffInfo);
            return true;
        }

    }
}
