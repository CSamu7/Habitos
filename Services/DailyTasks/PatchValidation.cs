using Habits.Models;
using System;

namespace Habits.Services.DailyTasks
{
    public class PatchValidation
    {
        public Result<DailyTask> Validate(DailyTask? dailyTask)
        {
            TimeSpan limitHours = new TimeSpan(24, 0, 0);
            DateTimeOffset now = DateTimeOffset.UtcNow;
            //404: Not Found
            if (dailyTask is null) return Result<DailyTask>.Failure(Status.NotFound, "La tarea diaria no fue encontrada");

            //400: Bad Request
            if (now.Subtract(limitHours) > dailyTask.Date)
                return Result<DailyTask>.Failure(Status.InvalidData, "Esta tarea ya no esta disponible");

            return Result<DailyTask>.Success(dailyTask);
        }
    }
}
