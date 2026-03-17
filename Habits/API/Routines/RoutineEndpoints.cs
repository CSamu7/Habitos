using Habits.API.Routines.DTO;
using Habits.Common;
using Habits.Models;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Habits.API.Routines
{
    public static class RoutineEndpoints
    {
        public static async Task<IResult> GetRoutine(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.GetRoutine(idRoutine);

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                GetRoutineResponse response = new GetRoutineResponse(routine.IdRoutine, routine.Name, routine.Minutes, routine.IdCategory); 
                return TypedResults.Ok<GetRoutineResponse>(response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PostRoutine(string username, PostRoutineRequest body, LinkGenerator generator, RoutineService service)
        {
            Result<Routine> result = await service.PostRoutine(username, body);
            //TODO: IsSameUser Authorization

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                GetRoutineResponse response = new GetRoutineResponse(routine.IdRoutine, routine.Name, routine.Minutes, routine.IdCategory);
                string? uri = generator.GetPathByName
                    ("getTask", new() { { "idTask", response.Id } });

                return TypedResults.Created<GetRoutineResponse>(uri, response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchRoutine(
            int idRoutine, 
            [FromBody] JsonElement jsonElement,
            RoutineService service)
        {
            var json = jsonElement.GetRawText();
            var doc = JsonConvert.DeserializeObject<JsonPatchDocument>(json)?.Sanitize();

            try
            { 
                var result = await service.PatchTask(idRoutine, doc);
                return Results.Ok();
            } catch (JsonPatchException ex)
            {
                return Results.Problem(ex.Message, statusCode: 400);
            }
        }
        public static async Task<IResult> GetAllRoutines(string username, RoutineService service)
        {
            //TODO: IsSameUser Authorization
            Result<List<Routine>> result = await service.GetAllRoutines(username);

            if (result.Status.Equals(Status.Ok))
            {
                List<GetRoutineResponse> tasks = result.Value.Select(task => new GetRoutineResponse(task.IdRoutine, task.Name, task.Minutes, task.IdCategory)).ToList();
                return TypedResults.Ok<List<GetRoutineResponse>>(tasks);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> DeleteTask(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.DeleteTask(idRoutine);

            return Results.Ok();
        }
    }
}
