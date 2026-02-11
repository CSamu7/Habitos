using Habits.API.Tasks.DTO;
using Habits.Models;
using Habits.Services.Tasks;
using System;

namespace Habits.API.Tasks
{ 
    public static class TasksEndpoints
    {
        public static async Task<IResult> GetTask(int idTask, TaskService service)
        {
            Result<Habits.Models.Task> result = await service.GetTask(idTask);

            if (result.Status.Equals(Status.Ok))
            {
                GetTaskResponse response = new GetTaskResponse(result.Value);
                return TypedResults.Ok<GetTaskResponse>(response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PostTask(int idUser, PostTaskRequest body, LinkGenerator generator, TaskService service)
        {
            Result<Habits.Models.Task> result = await service.PostTask(idUser, body);

            if (result.Status.Equals(Status.Ok))
            {
                GetTaskResponse response = new GetTaskResponse(result.Value);
                string? uri = generator.GetPathByName
                    ("getTask", new() { { "idTask", response.Id } });

                return TypedResults.Created< GetTaskResponse>(uri, response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> ModifyTask(int idTask, TaskService service)
        {
            return Results.Ok();
        }
        public static async Task<IResult> GetAllTasks(int idUser, TaskService service)
        {
            Result<List<Habits.Models.Task>> result = await service.GetAllTasks(idUser);

            if (result.Status.Equals(Status.Ok))
            {
                List<GetTaskResponse> tasks = result.Value.Select(task => new GetTaskResponse(task)).ToList();
                return TypedResults.Ok<List<GetTaskResponse>>(tasks);
            }

            return result.ToHttpResponse();
        }

        public static async Task<IResult> DeleteTask(int idTask, TaskService service)
        {
            Result<Habits.Models.Task> result = await service.DeleteTask(idTask);

            if (result.Status.Equals(Status.Ok))
                return TypedResults.NoContent();

            return Results.Ok();
        }
    }
}
