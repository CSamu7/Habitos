using FluentValidation;

namespace Habits.API.DailyRoutines.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, int> MinutesAreValid<T>
            (this IRuleBuilder<T, int> ruleBuilder)
        {
            int MAX_MINUTES = 480;

            return ruleBuilder
                .GreaterThan(0).WithMessage("Minutes must be positive")
                .LessThan(MAX_MINUTES).WithMessage("You can't add more than 8 hours");
        }
    }
}
