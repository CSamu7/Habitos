using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class Group
{
    public int IdGroup { get; set; }

    public string Name { get; set; } = null!;

    public int Color { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
