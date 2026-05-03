namespace Habits.Models;

public partial class Routine
{
    public int IdRoutine { get; set; }

    public string Name { get; set; } = null!;

    public int Minutes { get; set; }

    public bool IsActive { get; set; }

    public string IdUser { get; set; } = null!;

    public int? IdRoutineCategory { get; set; }

    public virtual RoutineCategory? IdRoutineCategoryNavigation { get; set; }
    public virtual User IdUserNavigation { get; set; } = null!;
}
