using Habits.Features.DailyTasks;
using Habits.Features.Tasks.Models;
using Habits.Features.Tasks.Validations;

namespace UnitTesting
{
    public class DailyTaskPatchValidationTests
    {
        [Theory]
        [InlineData(-4)]
        [InlineData(0)]
        public void Reject_negative_minutes(int minutes)
        {
            var validation = new DailyTaskPatchValidation();
            var request = new DailyTaskPatchRequest(minutes, DateTimeOffset.UtcNow, PatchOperations.Add);

            //Usar Result tambien aqui?
            Dictionary<string, string[]> result = validation.Validate(request);

            Assert.Single(result);
        }
        [Fact]
        public void Accept_natural_minutes()
        {
            var validation = new DailyTaskPatchValidation();
            var request = new DailyTaskPatchRequest(10, DateTimeOffset.UtcNow, PatchOperations.Add);

            Dictionary<string, string[]> result = validation.Validate(request);

            Assert.Empty(result);
        }
    }
}
