using FluentValidation.TestHelper;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Validation;

namespace Testing.UnitTesting.API.Validations
{
    public class PostTaskRequestValidationTests
    {
        [Fact]
        public void Empty_name_is_invalid()
        {
            PostRoutineRequestValidation sut = new PostRoutineRequestValidation();
            PostRoutineRequest request = new PostRoutineRequest { Name = "" };

            var result = sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        public void Repetition_is_less_than_seven_days()
        {

        }
    }
}
