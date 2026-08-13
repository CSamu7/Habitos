using FluentValidation;
using Habits.API.DailyRoutines.DTO;

namespace Habits.API.DailyRoutines.Validation
{
    public class DailyRoutineQueryParamsValidation : AbstractValidator<GetDailyRoutineQueryParams>
    {
        public DailyRoutineQueryParamsValidation(TimeProvider timeProvider)
        {
            DateTimeOffset now = timeProvider.GetUtcNow();

            RuleFor(x => x.DateEnd).LessThanOrEqualTo(now)
                .WithMessage("Date end must be in the present.");

            RuleFor(x => x.DateStart)
                .LessThanOrEqualTo(now)
                .WithMessage("Date start must be in the present.")
                .Must((filters, dateStart) => filters.DateEnd > filters.DateStart)
                .WithMessage("Start date must be before date end");
        }
    }
}
