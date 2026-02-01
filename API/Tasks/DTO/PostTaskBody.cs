using Habits.Models;
using Task = Habits.Models.Task;

namespace Habits.API.Tasks.DTO
{
    public class PostTaskBody
    {
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int RepeatedEvery { get; set; }
        public byte[]? UnavailableDays { get; set; }
        public int? IdGroup { get; set; } = null;
    }

    public static class PostTaskBodyExtensions
    {
        public static Task ToTask(this PostTaskBody body, int idUser)
        {
            Task task = new Task
            {
                IdUser = idUser,
                Name = body.Name,
                Minutes = body.Minutes,
                RepeatedEvery = body.RepeatedEvery,
                IdGroup = body.IdGroup,
                UnavailableDays = body.UnavailableDays,
                IsActive = true
            };

            return task;
        }
    }
}
