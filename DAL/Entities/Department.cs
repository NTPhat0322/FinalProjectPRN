using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StaffInfo> StaffInfos { get; set; } = new List<StaffInfo>();
}
