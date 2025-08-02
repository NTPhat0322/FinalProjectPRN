using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Schedule
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public DateTime ScheduleTime { get; set; }

    public int ServiceId { get; set; }

    public int? DoctorId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Service? Service { get; set; } 

    public virtual Account? Doctor { get; set; }
}
