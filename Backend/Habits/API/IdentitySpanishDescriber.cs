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
    }
}
