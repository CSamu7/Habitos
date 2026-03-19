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
                .WithMessage("You can't enter less than 50%")
                .LessThanOrEqualTo((byte)100)
                .WithMessage("You can't enter greather than 100%");
        }
    }
}
