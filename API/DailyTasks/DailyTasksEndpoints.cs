using Habits.API.DailyTasks.DTO;
using Habits.Models;
using Habits.Services.DailyTasks;
using Microsoft.AspNetCore.Mvc;

namespace Habits.API.DailyTasks
{
    public class DailyTasksEndpoints()
    {
        public static IResult GetDailyTask(int idDailyTask, DailyTaskService service)
        {
            //TODO: Realizar esta función
            return Results.Ok();
        }
        public static IResult GetDailyTasks(int idUser, GetAllDailyTasksQueryParams filters, DailyTaskService service)
        {
            Result<List<DailyTask>> result = service.GetDailyTasks(idUser, filters);

            if (result.Status.Equals(Status.Ok))
            {
                GetAllDailyTasksResponse reds = new GetAllDailyTasksResponse(result.Value);
                return Results.Ok(reds);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchMinutes(int idDailyTask, PatchDailyTaskRequest body, DailyTaskService service)
        {
            Result<DailyTask> result = body.Operation switch
            {
                PatchOperations.Add => await service.PatchMinutes(idDailyTask, body),
                _ => await service.PatchMinutes(idDailyTask, body),
            };

            return result.ToHttpResponse();
        }
    }
}
