namespace Habits.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public int Color { get; set; }

    public string IdUser { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();
}
