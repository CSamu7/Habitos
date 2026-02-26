using FluentValidation.TestHelper;
using Habits.API.Routines.DTO;

namespace Testing.UnitTesting.API.Validations
{
    public class PostTaskRequestValidationTests
    {
        [Fact]
        public void Empty_name_is_invalid()
        {
            PostRoutineRequestValidation validationRules = new PostRoutineRequestValidation();
            PostRoutineRequest request = new PostRoutineRequest { Name = "" };

            var result = validationRules.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        public void Repetition_is_less_than_seven_days()
        {

        }
    }
}
