namespace Habits.Features.Tasks.Models
{
    public record SimpleTaskDTO(
        int IdTask,
        string Name
    );
    public class ListTasksGetResponse
    {
        public required List<TaskGetResponse> Tasks;
    }
    public class TaskGetResponse
    {
        public int IdTask { get; set; }
        public required string Name { get; set; }
        public int Minutes { get; set; }
        public int RepeatedEvery { get; set; }
        public required List<bool> UnavailableDays { get; set; }
        public int? IdGroup { get; set; }
    }

    //TODO: Ver que hacer con esta clase.

    //public static class TaskExtensions
    //{
    //    public static TaskGetDTO Map(this Task task)
    //    {
    //        //Metodo. GetDaysList?
    //        List<bool> days = Enumerable.Repeat(false, 7).ToList();

    //        string unavailableDays = Convert.ToString(task.UnavailableDays[0], 2);
    //        int day = 0;

    //        foreach(char b in unavailableDays)
    //        {
    //            if (b == '1') days[day] = true;
    //            day++;
    //        }

    //        return new TaskGetDTO
    //        {
    //            idTask = task.IdTask,
    //            name = task.Name,
    //            minutes = task.Minutes,
    //            repeatedEvery = task.RepeatedEvery,
    //            unavailableDays = days,
    //            idGroup = task.IdGroup
    //        };
    //    }
    //}
}
