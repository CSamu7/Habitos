using FluentValidation;
using Habits.API.Users.DTO;

namespace Habits.API.Users.Validations
{
    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("This is not a valid email");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("You can't enter an empty username");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("You can't enter an empty password");

            RuleFor(x => x.MinGoal)
                .GreaterThanOrEqualTo((byte)50)
                .WithMessage("You can't enter a percentage less than 50%")
                .LessThanOrEqualTo((byte)100)
                .WithMessage("You can't enter a percentage greather than 100%");

            RuleFor(x => x.CutOffTime)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(23)
                .WithMessage("You must enter a hour between 00:00 and 23:00");

            RuleFor(x => x.Timezone)
                .Custom((x, context) =>
                {
                    try
                    {
                        var id = TimeZoneInfo.FindSystemTimeZoneById(x);
                    }
                    catch(TimeZoneNotFoundException _)
                    {
                        context.AddFailure("This timezone doesn't exist");
                    }
                });
        }
    }
}
