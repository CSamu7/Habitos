using Habits.Models;

namespace Habits.Features.Tasks.Validations
{
    //Negocios
    //TODO: Change name
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
    public class DailyTaskGetValidation() : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            List<string> errors = new List<string>();

            var dateStart = context.GetArgument<DateOnly?>(1);
            var dateEnd = context.GetArgument<DateOnly?>(2);

            if (dateEnd < dateStart)
                errors.Add("La fecha final no puede ser menor a la fecha de inicio");

            if (dateEnd > now)
                errors.Add("La fecha final debe ser una fecha valida");

            if (errors.Count > 0)

                return Results.ValidationProblem(
                    new Dictionary<string, string[]>()
                    {
                        {"Fecha", errors.ToArray() }
                    }
                );

            return await next(context);
        }
    }
}
