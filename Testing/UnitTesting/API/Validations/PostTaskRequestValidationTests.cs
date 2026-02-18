using FluentValidation.TestHelper;
using Habits.API.Tasks.DTO;
using Habits.API.Tasks.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.UnitTesting.API.Validations
{
    public class PostTaskRequestValidationTests
    {
        [Fact]
        public void Empty_name_is_invalid()
        {
            PostTaskRequestValidation validationRules = new PostTaskRequestValidation();
            PostTaskRequest request = new PostTaskRequest { Name = "" };

            var result = validationRules.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        public void Repetition_is_less_than_seven_days() { 
            
        }
    }
}
