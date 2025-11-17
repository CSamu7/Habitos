using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTimeOffset LastSession { get; set; }

    public decimal MinGoal { get; set; }

    public int FreeTime { get; set; }

    public int Streak { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
