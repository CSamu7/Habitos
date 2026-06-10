using Microsoft.AspNetCore.Identity;

namespace Habits.API
{
    public class IdentitySpanishDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "Password",
                Description = "La contraseña debe tener mas de 8 caracteres"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "Email",
                Description = "El correo ya esta en uso"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "Username",
                Description = $"El nombre de usuario {userName} ya esta en uso"
            };
        }

        public override IdentityError InvalidUserName(string? userName)
        {
            return new IdentityError()
            {
                Code = "Username",
                Description = "El nombre de usuario solo puede tener letras o digitos"
            };
        }
    }
}
