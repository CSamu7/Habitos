using Habits.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<DailyTask> tasks = service.GetDailyTasks
                (idUser, new GetAllDailyTaskFilters(null, null, null));

            DailyTaskList unfinishedTasks = new DailyTaskList(tasks);
            return TypedResults.Ok(
                new DailyTasksStats<GetTodayDailyTaskDTO>(
                    unfinishedTasks.Tasks
                    .Where(task => task.GetProgress() == DailyTaskProgress.NotDone)
                    .Select(task => task.MapTodayDTO())
                    .ToList(),
                    unfinishedTasks.GetTotalMinutes(),
                    unfinishedTasks.GetMinutesCompleted(),
                    unfinishedTasks.GetTotalMinutes() - unfinishedTasks.GetMinutesCompleted(),
                    unfinishedTasks.GetTotalPercentage()
            ));
        }
        public IResult GetDailyTasks(int idUser, DateOnly? dateStart, DateOnly? dateEnd, DailyTaskProgress? status, DailyTaskService service)
        {
            var filters = new GetAllDailyTaskFilters(dateStart, dateEnd, status);
            var list = service.GetDailyTasks(idUser, filters);

            return Results.Ok(
                new ResponseBase<GetDailyTaskDTO>(
                    list.Select(task => task.Map()).ToList(),
                    list.Count()));
        }
        public async Task<IResult> PatchMinutes(int idDailyTask, PatchDailyTask body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyTask, body, service.AddMinutes),
                PatchOperations.Replace => await service.PatchMinutes(idDailyTask, body, service.ReplaceMinutes),
            };

            return Results.Ok();
        }
    }
    public enum PatchOperations { Add, Replace }
    public enum DailyTaskProgress { NotDone, InProgress, Done, Incomplete, Overdone}
}
