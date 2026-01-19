using Habits.Features.DailyTasks.Models;
using Habits.Features.Tasks;
using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks.Endpoints
{
    public class DailyTasksEndpoints()
    {
        public IResult GetTodayDailyTasks(int idUser, DailyTaskService service)
        {
            //Estudiar Inversion de Control.
            Result<List<DailyTask>> result = service.GetDailyTasks
                (idUser, new GetAllDailyTaskFilters(null, null, null, TimeProvider.System));

            return result.ToHttpResponse();
        }
        public IResult GetDailyTasks(int idUser, DateOnly? dateStart, DateOnly? dateEnd, DailyTaskProgress? status, DailyTaskService service)
        {
            var filters = new GetAllDailyTaskFilters(dateStart, dateEnd, status, TimeProvider.System);
            Result<List<DailyTask>> result = service.GetDailyTasks(idUser, filters);

            return result.ToHttpResponse();
        }
        public async Task<IResult> PatchMinutes(int idDailyTask, DailyTaskPatchRequest body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyTask, body),
                _ => await service.PatchMinutes(idDailyTask, body),
            };

            return result.ToHttpResponse();
        }
    }
    public enum DailyTaskProgress { NotDone, InProgress, Done, Incomplete, Overdone }
}
