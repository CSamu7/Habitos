using FluentValidation.TestHelper;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;

namespace Testing.UnitTesting.API.Validations
{
    public class PatchUserRequestValidationTests
    {
        [Fact]
        public void Min_goal_is_between_50_and_100()
        {
            PatchUserRequestValidation sut = new();
            PatchUserRequest dto = new("UTC-11", 12, 25);

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.MinGoal);
        }

        [Fact]
        public void Timezone_is_valid()
        {
            PatchUserRequestValidation sut = new();
            PatchUserRequest dto = new("UTC-134", 24, 12);

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Timezone);
        }

        [Fact]
        public void CutOffTime_is_between_0_and_23()
        {
            PatchUserRequestValidation sut = new();
            PatchUserRequest dto = new("UTC-11",25, 60);

            var result = sut.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.CutOffTime);
        }
    }
}
