using FluentValidation;
using Habits.API.Users.DTO;

namespace Habits.API.Users.Validations
{
    public class PatchUserRequestValidation : AbstractValidator<PatchUserRequest>
    {
        public PatchUserRequestValidation()
        {

            RuleFor(x => x.MinGoal)
                .GreaterThanOrEqualTo((byte)50)
                .WithMessage("")
                .LessThanOrEqualTo((byte)100)
                .WithMessage("You can't enter a percentage greather than 100%");

            RuleFor(x => x.CutOffTime)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(23)
                .WithMessage("You must enter a hour between 00:00 and 23:00");

            RuleFor(x => x.Timezone)
                .Custom((x, context) =>
                {
                    if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(x, out _))
                        context.AddFailure("This timezone doesn't exist");
                });
        }
    }
}
