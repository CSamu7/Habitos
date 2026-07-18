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
                .NotEmpty()
                .WithMessage("El nombre esta vacio")
                .MaximumLength(40)
                .WithMessage("El nombre es muy largo.");

            RuleFor(x => x.Minutes).MinutesAreValid();
        }
    }
}
