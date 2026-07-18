using FluentValidation.TestHelper;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;

namespace Testing.UnitTesting.API.Validations
{
    public class RegisterUserRequestValidationTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("notvalid.com")]
        public void Email_contains_at(string email) //contains @
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", email, "");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Password_is_not_empty()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
