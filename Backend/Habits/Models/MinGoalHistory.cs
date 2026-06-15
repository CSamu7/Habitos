namespace Habits.Models
{
    public class MinGoalHistory
    {
        public int IdMinGoalHistory { get; set; }
        public string IdUser { get; set; } = null!;
        public int MinGoal { get; set; }
        public DateTimeOffset ChangedAt { get; set; }
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
