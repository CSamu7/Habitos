using FluentValidation;

namespace Habits.API.DailyTasks.Validation
{
    public class MinutesValidation : AbstractValidator<int>
    {
        public MinutesValidation()
        {
            RuleFor(x => x)
                .LessThanOrEqualTo(0)
                .WithMessage("Minutes can't be negative")
                .GreaterThan(480)
                .WithMessage("You only can enter under 8 hours");
        }
    }
}
