using FluentValidation.TestHelper;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Validation;
using Habits.Services.DailyRoutines;

namespace Testing.UnitTesting.API.Validations
{
    public class PatchDailyRoutineValidationTests
    {
        [Fact]
        public void Negative_minutes_are_invalid()
        {
            PatchDailyTaskValidation sut = new PatchDailyTaskValidation();
            PatchDailyRoutineRequest request = new PatchDailyRoutineRequest(
                -4,
                 new DateTime(2000, 1, 1),
                 PatchOperations.Add
            );

            var result = sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(request => request.Minutes)
                .WithErrorMessage("Minutes must be positive")
                .Only();
        }

        [Fact]
        public void Minutes_greather_than_8_hours_are_invalid()
        {
            PatchDailyTaskValidation sut = new PatchDailyTaskValidation();
            PatchDailyRoutineRequest request = new PatchDailyRoutineRequest(
                8 * 60,
                new DateTime(2000, 1, 1),
                PatchOperations.Add
            );

            var result = sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(request => request.Minutes)
                .WithErrorMessage("You can't add more than 8 hours")
                .Only();
        }
    }
}