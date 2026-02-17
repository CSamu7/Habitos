using Habits.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Habits.Features.DailyTasks.Filters
{
    public class DailyTaskGetFilters() : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var dateStart = context.GetArgument<DateOnly?>(1);
            var dateEnd = context.GetArgument<DateOnly?>(2);

            List<string> dateErrors = ValidateDate(dateStart, dateEnd);

            if (dateErrors.Count > 0)
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>()
                    {
                        {"Fecha", dateErrors.ToArray() }
                    }
                );

            return await next(context);
        }
        private List<string> ValidateDate(DateOnly? dateStart, DateOnly? dateEnd)
        {
            List<string> errors = [];
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);

            if (dateEnd < dateStart)
                errors.Add("La fecha final no puede ser menor a la fecha de inicio");

            if (dateEnd > now)
                errors.Add("La fecha final debe ser una fecha valida");

            return errors;
        }
    }
}
