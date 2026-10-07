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
                .GreaterThan(0).WithMessage("Minutos deben ser positivos")
                .LessThanOrEqualTo(Constants.MAX_FULL_ROUTINE_MINUTES).WithMessage("No puedes añadir mas de 8 horas en una tarea.");
        }
    }
}
