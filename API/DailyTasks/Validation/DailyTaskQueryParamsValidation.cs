using FluentValidation;
using Habits.API.DailyTasks.DTO;

namespace Habits.API.DailyTasks.Validation
{
    public class DailyTaskQueryParamsValidation : AbstractValidator<GetAllDailyTasksQueryParams>
    {
        public DailyTaskQueryParamsValidation(TimeProvider timeProvider)
        {
            RuleFor(x => x.DateEnd).DateMustBeInPresent();

            RuleFor(x => x.DateStart)
                .DateMustBeInPresent()
                .Must((filters, dateStart) => filters.DateEnd > filters.DateStart)
                .WithMessage("Start date must be before date end");
        }
    }
}
