namespace Habits.Models;

public partial class Schedule
{
    public int IdSchedule { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Days { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<SchedulesTask> SchedulesTasks { get; set; } = new List<SchedulesTask>();
}
