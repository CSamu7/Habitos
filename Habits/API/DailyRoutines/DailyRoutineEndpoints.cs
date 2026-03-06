using Habits.API.DailyRoutines.DTO;
using Habits.Models;
using Habits.Services.DailyRoutines;

namespace Habits.API.DailyRoutines
{
    public class DailyRoutineEndpoints()
    {
        public static async Task<IResult> GetDailyRoutine(int idDailyRoutine, DailyRoutineService service)
        {
            Result<DailyRoutine> result = await service.GetRoutine(idDailyRoutine);

            if (result.Status.Equals(Status.Ok))
            {
                return Results.Ok(GetDailyRoutineResponse.FromDailyTask(result.Value));
            }

            return result.ToHttpResponse();
        }
        public static IResult GetDailyRoutines(string username, GetDailyRoutineQueryParams filters, DailyRoutineService service)
        {
            Result<List<DailyRoutine>> result = service.GetRoutines(username, filters);

            if (result.Status.Equals(Status.Ok))
            {
                GetAllDailyRoutinesResponse res = new GetAllDailyRoutinesResponse(result.Value);
                return Results.Ok(res);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchMinutes(int idDailyRoutine, PatchDailyRoutineRequest body, DailyRoutineService service)
        {
            Result<DailyRoutine> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyRoutine, body),
                _ => await service.PatchMinutes(idDailyRoutine, body),
            };

            return result.ToHttpResponse();
        }
    }
}
