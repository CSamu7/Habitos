using Habits.API.DailyRoutines.DTO;
using Habits.Models;
using Habits.Services.DailyRoutines;

namespace Habits.API.DailyRoutines
{
    public class DailyRoutineEndpoints()
    {
        public static async Task<IResult> GetDailyRoutine(int idDailyRoutine, DailyTaskService service)
        {
            Result<DailyTask> result = await service.Get(idDailyRoutine);

            return result.Status.Equals(Status.Ok)
                ? TypedResults.Ok(result.Value.ToGetDailyRoutineResponse())
                : result.ToHttpResponse();
        }
        public static async Task<IResult> GetDailyRoutines(string username, GetDailyRoutineQueryParams filters, DailyTaskService service)
        {
            Result<List<DailyTask>> result = await service.GetAll(username, filters);

            return result.Status.Equals(Status.Ok)
                ? TypedResults.Ok(result.Value.ToGetAllDailyRoutinesResponse())
                : result.ToHttpResponse();
        }
        public static async Task<IResult> PatchMinutes(int idDailyRoutine, PatchDailyRoutineRequest body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyRoutine, body),
                _ => await service.PatchMinutes(idDailyRoutine, body),
            };

            return result.ToHttpResponse();
        }
    }
}
