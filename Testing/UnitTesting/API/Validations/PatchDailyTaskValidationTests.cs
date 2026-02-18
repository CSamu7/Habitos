using FluentValidation.TestHelper;
using Habits.API.DailyTasks.DTO;
using Habits.API.DailyTasks.Validation;
using Habits.Services.DailyTasks;

namespace Testing.UnitTesting.API.Validations
{
    public class PatchDailyTaskValidationTests
    {
        [Fact]
        public void Negative_minutes_are_invalid()
        {
            PatchDailyTaskValidation validation = new PatchDailyTaskValidation();
            PatchDailyTaskRequest request = new PatchDailyTaskRequest 
            { 
                FinishedAt = new DateTime(2000,1,1), 
                Minutes = -4, 
                Operation = PatchOperations.Add
            };

            var result = validation.TestValidate(request);

            result.ShouldHaveValidationErrorFor(request => request.Minutes)
                .WithErrorMessage("Minutes must be positive")
                .Only();
        }

        [Fact]
        public void Minutes_greather_than_8_hours_are_invalid()
        {
            PatchDailyTaskValidation validation = new PatchDailyTaskValidation();
            PatchDailyTaskRequest request = new PatchDailyTaskRequest
            {
                FinishedAt = new DateTime(2000, 1, 1),
                Minutes = (8 * 60),
                Operation = PatchOperations.Add
            };

            var result = validation.TestValidate(request);

            result.ShouldHaveValidationErrorFor(request => request.Minutes)
                .WithErrorMessage("You can't add more than 8 hours")
                .Only();
        }
    }
}