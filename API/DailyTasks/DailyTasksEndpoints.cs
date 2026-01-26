using Habits.API.DailyTasks.DTO;
using Habits.Models;
using Habits.Services.DailyTasks;
using Microsoft.AspNetCore.Mvc;

namespace Habits.API.DailyTasks
{
    public class DailyTasksEndpoints()
    {
        public IResult GetTodayDailyTasks(int idUser, GetAllFilters today, DailyTaskService service)
        {
            Result<List<DailyTask>> result = service.GetDailyTasks
                (idUser, today); 

            if (result.Status.Equals(Status.Ok))
            {
                GetAllResponse reds = new GetAllResponse(result.Value);
                return Results.Ok(reds);
            }

            return result.ToHttpResponse();
        }
        public IResult GetDailyTasks(int idUser, GetAllFilters filters, DailyTaskService service)
        {
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
}
