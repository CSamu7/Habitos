using FluentValidation;
using Habits.API.DailyTasks.Validation;
using Habits.API.Tasks.DTO;

namespace Habits.API.Tasks.Validation
{
    public class PostTaskRequestValidation : AbstractValidator<PostTaskRequest>
    {
        public PostTaskRequestValidation() {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name can't be null")
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Minutes).MinutesAreValid();

            RuleFor(x => x.RepeatedEvery)
                .GreaterThan(0)
                .LessThanOrEqualTo(7)
                .WithMessage("A habit must be repeated every 7 days maximum");
        }
    }
}
