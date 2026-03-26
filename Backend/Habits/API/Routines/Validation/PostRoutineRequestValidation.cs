using FluentValidation;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Routines.DTO;

namespace Habits.API.Routines.Validation
{
    public class PostRoutineRequestValidation : AbstractValidator<PostRoutineRequest>
    {
        public PostRoutineRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name can't be null")
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Minutes).MinutesAreValid();
        }
    }
}
