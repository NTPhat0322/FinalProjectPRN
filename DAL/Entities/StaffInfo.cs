using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StaffInfo
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int DepartmentId { get; set; }

    public string? Degree { get; set; }

    public int? YearOfExperience { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
