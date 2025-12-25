using Habits.Models;

namespace Habits.Features.Tasks
{
    public class DailyTaskList
    {
        public List<DailyTask> Tasks { get; }
        public DailyTaskList(List<DailyTask> tasks) {
            Tasks = tasks;
        }
        public int GetMinutesCompleted() =>
            Tasks.Aggregate(0, (acc, task) => acc += task.MinutesCompleted);
        public int GetTotalMinutes() =>
            Tasks.Aggregate(0, (acc, task) => acc += task.TotalMinutes);
        public string GetTotalPercentage()
        {
            double percentage = ((double)GetMinutesCompleted() / GetTotalMinutes()) * 100;
        
            return Double.IsFinite(percentage) 
                ? $"{percentage.ToString("00.00")}%" 
                : "0.00%";
        }
        public DailyTaskList GetUnfinishedTasks() =>
            new DailyTaskList(
                Tasks.Where(task => task.CompletedAt is null).ToList()
            );
    }
    public class DailyTasksEndpoints()
    {
        public IResult GetTodayDailyTasks(int idUser, DailyTaskService service)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;
            DailyTaskList tasks = service.GetDailyTasks(idUser, new TaskFilters(null, today));
            DailyTaskList unfinishedTasks = tasks.GetUnfinishedTasks();

            return TypedResults.Ok(
                new DailyTasksStats<GetTodayDailyTaskDTO>(
                    unfinishedTasks.Tasks.Select(
                        task => task.MapTodayDTO()   
                    ).ToList(),
                    tasks.GetTotalMinutes(),
                    tasks.GetMinutesCompleted(),
                    tasks.GetTotalMinutes() - tasks.GetMinutesCompleted(),
                    tasks.GetTotalPercentage()
            ));
        }
        public IResult GetDailyTasks(int idUser, DateTimeOffset? start, DateTimeOffset? end, DailyTaskService service)
        {
            var list = service.GetDailyTasks(idUser, new TaskFilters(start, end));

            return TypedResults.Ok(
                new ResponseBase<GetDailyTaskDTO>(
                    list.Tasks.Select(task => task.Map()).ToList(),
                    list.Tasks.Count()));
        }
        public async Task<IResult> PatchMinutes(int idDailyTask, PatchDailyTask body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyTask, body, service.AddMinutes),
                PatchOperations.Replace => await service.PatchMinutes(idDailyTask, body, service.ReplaceMinutes),
            };

            return result.ToProblem();
        }

    }
    public enum PatchOperations { Add, Replace }
}
