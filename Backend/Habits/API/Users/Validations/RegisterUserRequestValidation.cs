using FluentValidation;
using Habits.API.Users.DTO;

namespace Habits.API.Users.Validations
{
    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo esta vacio")
                .EmailAddress().WithMessage("El correo no es valido");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario esta vacio")
                .MinimumLength(4).WithMessage("El nombre de usuario debe tener mas de 4 caracteres");

            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña esta vacia");
        }
    }
}
