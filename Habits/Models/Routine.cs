namespace Habits.Models;
public partial class Routine
{
    public int IdRoutine { get; set; }

    public string Name { get; set; } = null!;

    public int Minutes { get; set; }

    public bool IsActive { get; set; }

    public string IdUser { get; set; } = null!;

    public int? IdCategory { get; set; }

    public virtual ICollection<DailyRoutine> DailyRoutines { get; set; } = new List<DailyRoutine>();

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
