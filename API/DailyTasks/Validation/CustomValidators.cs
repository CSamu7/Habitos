using FluentValidation;

namespace Habits.API.DailyTasks.Validation
{
    public static class CustomValidators 
    {
        public static IRuleBuilderOptions<T, DateOnly> DateMustBeInPresent<T>
            (this IRuleBuilder<T, DateOnly> ruleBuilder)
        {
            return ruleBuilder.LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("{PropertyName} must be in the present.");
        }
        public static IRuleBuilderOptions<T, int> MinutesAreValid<T>
            (this IRuleBuilder<T, int> ruleBuilder)
        {
            int MAX_MINUTES = 480;

            return ruleBuilder
                .GreaterThan(0).WithMessage("Minutes must be positive")
                .LessThanOrEqualTo(MAX_MINUTES).WithMessage("You can't add more than 8 hours");
        }
    }
}
