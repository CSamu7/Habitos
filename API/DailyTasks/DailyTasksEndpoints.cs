using Habits.API.DailyTasks.DTO;
using Habits.Models;
using Habits.Services.DailyTasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Habits.API.DailyTasks
{
    public class DailyTasksEndpoints()
    {
        public static async Task<IResult> GetDailyTask(int idDailyTask, DailyTaskService service)
        {
            Result<DailyTask> result = await service.GetDailyTask(idDailyTask);

            if (result.Status.Equals(Status.Ok))
            {
                return Results.Ok(GetDailyTaskResponse.FromDailyTask(result.Value));
            }

            return result.ToHttpResponse();
        }
        public static IResult GetDailyTasks(int idUser, GetAllDailyTasksQueryParams filters, DailyTaskService service)
        {
            Result<List<DailyTask>> result = service.GetDailyTasks(idUser, filters);

            if (result.Status.Equals(Status.Ok))
            {
                GetAllDailyTasksResponse res = new GetAllDailyTasksResponse(result.Value);
                return Results.Ok(res);
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
