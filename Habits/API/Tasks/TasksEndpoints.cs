using Habits.API.Tasks.DTO;
using Habits.Common;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

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
        public static async Task<IResult> PatchTask(int idTask, [FromBody] JsonElement jsonElement, TaskService service, HttpRequest httpRequest)
        {
            var json = jsonElement.GetRawText();
            var doc = JsonConvert.DeserializeObject<JsonPatchDocument>(json);

            var newDoc = doc?.Sanitize();

            try
            {
                await service.PatchTask(idTask, newDoc);
                return Results.Ok();
            } catch (JsonPatchException ex)
            {
                return Results.Problem(ex.Message, statusCode: 400);
            }
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
