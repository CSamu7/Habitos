using Habits.Models;
using Task = Habits.Models.Task;

namespace Habits.API.Tasks.DTO
{
    public class PostTaskRequest
    {
        public required string Name { get; set; }
        public int Minutes { get; set; }
        public int RepeatedEvery { get; set; }
        public byte[]? UnavailableDays { get; set; }
        public int? IdGroup { get; set; } = null;
    }

    public static class PostTaskRequestExtensions
    {
        public static Task ToTask(this PostTaskRequest postTaskRequest)
        {
            return new Task
            {
                Name = postTaskRequest.Name,
                Minutes = postTaskRequest.Minutes,
                RepeatedEvery = postTaskRequest.RepeatedEvery,
                IdGroup = postTaskRequest.IdGroup,
                UnavailableDays = postTaskRequest.UnavailableDays,
                IsActive = true
            };
        }
    }
}
