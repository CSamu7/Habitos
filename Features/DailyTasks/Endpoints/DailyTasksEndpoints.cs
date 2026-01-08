using Habits.Features.Tasks;
using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks.Endpoints
{
    public class DailyTasksEndpoints()
    {
        public IResult GetTodayDailyTasks(int idUser, DailyTaskService service)
        {
            List<DailyTask> tasks = service.GetDailyTasks
                (idUser, new GetAllDailyTaskFilters(null, null, null));

            return TypedResults.Ok(new DailyTasksGetAllResponse(tasks));
        }
        public IResult GetDailyTasks(int idUser, DateOnly? dateStart, DateOnly? dateEnd, DailyTaskProgress? status, DailyTaskService service)
        {
            var filters = new GetAllDailyTaskFilters(dateStart, dateEnd, status);
            List<DailyTask> list = service.GetDailyTasks(idUser, filters);

            return Results.Ok(new DailyTasksGetAllResponse(list));
        }
        public async Task<IResult> PatchMinutes(int idDailyTask, DailyTaskPatchRequest body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyTask, body),
                _ => await service.PatchMinutes(idDailyTask, body),
            };

            return Results.Ok();
        }
    }
    public enum DailyTaskProgress { NotDone, InProgress, Done, Incomplete, Overdone }
}
