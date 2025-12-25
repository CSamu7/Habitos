using Habits.Models;

namespace Habits.Features.Tasks.Validations
{
    public class DailyTaskValidation
    {
        private readonly DailyTask? _dailyTask;
        public DailyTaskValidation(DailyTask? dailyTask) { 
            _dailyTask = dailyTask;
        }
        public Result<DailyTask> Validate()
        {
            TimeSpan limitHours = new TimeSpan(24, 0, 0);
            DateTimeOffset now = DateTimeOffset.UtcNow;

            //404: Not Found
            if (_dailyTask is null) return Result<DailyTask>.Failure(Status.NotFound, "La tarea diaria no fue encontrada");

            //400: Bad Request
            if (now.Subtract(limitHours) > _dailyTask.Date)
                return Result<DailyTask>.Failure(Status.InvalidData, "Esta tarea ya no esta disponible");

            return Result<DailyTask>.Success(_dailyTask);
        }

    }
}
