using FluentValidation;
using Habits.Common;
using Habits.Common.DailyTasks;
using System.Data;

namespace Habits.API.DailyTasks.Validation
{
    public class GetAllFiltersValidation : AbstractValidator<GetAllFilters>
    {
        public GetAllFiltersValidation()
        {
            RuleFor(x => x.DateStart.ToDateTimeOffset())
                .GreaterThan(DateTimeOffset.UtcNow)
                .WithMessage("Start date can't be in the future")
                .Must((filters, dateStart) => filters.DateEnd > filters.DateStart)
                .WithMessage("Start date must be before date end");

            RuleFor(x => x.DateEnd.ToDateTimeOffset())
                .GreaterThan(DateTimeOffset.Now)
                .WithMessage("End date can't be in the future");

            RuleFor(x => x.Progress)
                .IsInEnum()
                .WithMessage("The progression is not valid");
        }
    }


}
