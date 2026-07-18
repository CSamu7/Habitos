using FluentValidation;
using Habits.Common;

namespace Habits.API.DailyRoutines.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, int> MinutesAreValid<T>
            (this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage("Minutes must be positive")
                .LessThan(Constants.MAX_FULL_ROUTINE_MINUTES).WithMessage("You can't add more than 8 hours");
        }
    }
}
