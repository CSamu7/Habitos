using FluentValidation;
using Habits.API.DailyRoutines.DTO;

namespace Habits.API.DailyRoutines.Validation
{
    public class DailyRoutineQueryParamsValidation : AbstractValidator<GetDailyRoutineQueryParams>
    {
        public DailyRoutineQueryParamsValidation(TimeProvider timeProvider)
        {
            DateTimeOffset now = timeProvider.GetUtcNow();
            DateOnly nowDate = new DateOnly(now.Year, now.Month, now.Day);

            RuleFor(x => x.DateEnd).LessThanOrEqualTo(nowDate)
                .WithMessage("Date end must be in the present.");

            RuleFor(x => x.DateStart)
                .LessThanOrEqualTo(nowDate)
                .WithMessage("Date start must be in the present.")
                .Must((filters, dateStart) => filters.DateEnd > filters.DateStart)
                .WithMessage("Start date must be before date end");
        }
    }
}
