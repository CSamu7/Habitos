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
            RegisterUserRequest dto = new("Test", "valido@.com", "aaa", 24, 12, "UTC-11");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.MinGoal);
        }

        [Theory]
        [InlineData("")]
        [InlineData("notvalid.com")]
        public void Email_contains_at(string email) //contains @
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", email, "", 56, 12, "UTC-11");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Password_is_not_empty()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "", 24, 12, "UTC-11");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Timezone_is_valid()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "", 24, 12, "UTC-134");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Timezone);
        }

        [Fact]
        public void CutOffTime_is_between_0_and_23()
        {
            var sut = new RegisterUserRequestValidation();
            RegisterUserRequest dto = new("Test", "valido@.com", "", 65, 25, "UTC-11");

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.CutOffTime);
        }
    }
}
