
using Habits.Common;

namespace Habits.API.DailyTasks.Validation
{
    public class GetAllEndpointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            GetAllFilters filters = context.GetArgument<GetAllFilters>(1);
            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

            List<string> dateErrors = [];

            if (filters.DateStart > filters.DateEnd)
                dateErrors.Add("Date start must be before date end");

            //TODO: Bug, la fecha no es mayor porque empieza con 0:00 de tiempo.
            if (filters.DateEnd.ToDateTimeOffset() > DateTimeOffset.UtcNow)
                dateErrors.Add("Date end can't be in the future");

            if (filters.DateStart.ToDateTimeOffset() > DateTimeOffset.UtcNow)
                dateErrors.Add("Date start can't be in the future");

            if (dateErrors.Count > 0)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Date", dateErrors.ToArray() }
                });

            return await next(context);
        }
    }
}
