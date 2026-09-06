using Microsoft.AspNetCore.Identity;

namespace Habits.Models;
public partial class User : IdentityUser
{
    public bool IsActive { get; set; }
    public DateTimeOffset LastSession { get; set; }
    public byte MinGoal { get; set; }
    public int CutOffTime { get; set; }
    public int Streak { get; set; }
    public string TimeZone { get; set; } = null!;
    public virtual ICollection<Category> RoutineCategories { get; set; } = new List<Category>();
    public virtual ICollection<MinGoalHistory> MinGoalHistories { get; set; } = new List<MinGoalHistory>();
    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();
}
