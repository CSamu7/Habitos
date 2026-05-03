using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class RoutineCategory
{
    public int IdRoutineCategory { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();
    public virtual User? IdUserNavigation { get; set; }
}
