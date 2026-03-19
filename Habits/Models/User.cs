using Microsoft.AspNetCore.Identity;

namespace Habits.Models;
public partial class User : IdentityUser
{
    public bool IsActive { get; set; }
    public DateTimeOffset LastSession { get; set; }
    public byte MinGoal { get; set; }
    public DateTimeOffset CutOffTime { get; set; }
    public int Streak { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();
}
