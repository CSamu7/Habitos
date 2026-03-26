using FluentValidation.TestHelper;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Validation;

namespace Testing.UnitTesting.API.Validations
{
    public class PostRoutineRequestValidationTests
    {
        [Fact]
        public void Empty_name_is_invalid()
        {
            PostRoutineRequestValidation sut = new PostRoutineRequestValidation();
            PostRoutineRequest request = new PostRoutineRequest("", 10, null);

            var result = sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
