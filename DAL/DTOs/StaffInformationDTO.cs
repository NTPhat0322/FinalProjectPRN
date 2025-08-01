using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class StaffInformationDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int DepartmentId { get; set; }
        public string StaffName { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public string? Degree { get; set; }
        public int? YearOfExperience { get; set; }
    }
}
