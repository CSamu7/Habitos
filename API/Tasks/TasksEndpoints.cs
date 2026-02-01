using Habits.API.Tasks.DTO;
using Habits.Services.Tasks;

namespace Habits.API.Tasks
{
    public static class TasksEndpoints
    {
        public static async Task<IResult> PostTask(int idUser, PostTaskBody body, LinkGenerator generator, TaskService service)
        {
            Result<int> result = await service.PostTask(idUser, body);

            if (result.Status.Equals(Status.Ok))
            {
                string? link = generator.GetUriByName
                    ("getTask", values: new(idUser, result.Value), );
                return TypedResults.Created<Task>(link, null);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> ModifyTask(int idTask, TaskService service)
        {
            return Results.Ok();
        }
        public static async Task<IResult> GetTask(int idUser, int idTask)
        {
            return Results.Ok();
        }
        public static async Task<IResult> DeleteTask(int idTask, TaskService service)
        {
            return Results.Ok();
        }
    }
}
