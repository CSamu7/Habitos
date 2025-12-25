using System.Text.Json.Serialization;
using Task = Habits.Models.Task;

namespace Habits.Features.Tasks
{
    public class ListTasksGetDTO
    {
        public required List<TaskGetDTO> Tasks;
    }

    public class TaskGetDTO
    {
        public int idTask { get; set; }
        public required string name { get; set; }
        public int minutes { get; set; }
        public int repeatedEvery { get; set; }
        public required List<bool> unavailableDays { get; set; }
        public int? idGroup { get; set; }
    }
    public static class TaskExtensions
    {
        public static TaskGetDTO Map(this Task task)
        {
            List<bool> days = Enumerable.Repeat(false, 7).ToList();

            string unavailableDays = Convert.ToString(task.UnavailableDays[0], 2);
            int day = 0;

            foreach(char b in unavailableDays)
            {
                if (b == '1') days[day] = true;
                day++;
            }

            return new TaskGetDTO
            {
                idTask = task.IdTask,
                name = task.Name,
                minutes = task.Minutes,
                repeatedEvery = task.RepeatedEvery,
                unavailableDays = days,
                idGroup = task.IdGroup
            };
        }
    }

}
