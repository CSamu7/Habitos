using FluentValidation;
using Habits.API.Users.DTO;

namespace Habits.API.Users.Validations
{
    public class PatchUserRequestValidation : AbstractValidator<PatchUserRequest>
    {
        public PatchUserRequestValidation()
        {
            RuleFor(x => x.MinGoal)
                .GreaterThanOrEqualTo(50)
                .WithMessage("")
                .LessThanOrEqualTo(100)

                .WithMessage("Debes ingresar un porcentaje entre 50 y 100%");
            RuleFor(x => x.CutOffTime)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(23)
                .WithMessage("Debes entrar una hora entre 00:00 y 23:00");

            RuleFor(x => x.Timezone)
                .Custom((x, context) =>
                {
                    if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(x, out _))
                        context.AddFailure("No existe esta zona horaria");
                });
        }
    }
}
