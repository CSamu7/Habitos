using FluentValidation.TestHelper;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;

namespace Testing.UnitTesting.API.Validations
{
    public class RegisterUserRequestValidationTests
    {
        [Fact]
        public void Min_goal_is_between_50_and_100()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "aaa", 24, new DateTime(2020, 1, 1, 23, 11, 11));

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.MinGoal);
        }

        [Theory]
        [InlineData("")]
        [InlineData("notvalid.com")]
        public void Email_contains_at(string email) //contains @
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", email, "", 24, new DateTime(2020, 1, 1, 23, 11, 11));

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Password_is_not_empty()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "", 24, new DateTime(2020, 1, 1, 23, 11, 11));

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
