using FluentValidation.TestHelper;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Validation;
using Microsoft.Extensions.Time.Testing;

namespace Testing.UnitTesting.API.Validations
{
    public class DailyRoutineQueryParamsValidationTests
    {
        [Fact]
        public void End_date_is_after_start_date()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2020, 10, 1));
            GetDailyRoutineQueryParams queryParams =
                new(new DateOnly(2025, 1, 1), new DateOnly(2024, 1, 1), null);
            DailyRoutineQueryParamsValidation validation = new(timeProvider);

            var result = validation.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateStart)
                .WithErrorMessage("Start date must be before date end");
        }
        [Fact]
        public void Start_date_is_in_the_present()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2025, 12, 31));

            GetDailyRoutineQueryParams queryParams =
                new(new DateOnly(2950, 1, 1), new DateOnly(2024, 1, 1), null);
            DailyRoutineQueryParamsValidation validation = new(timeProvider);

            var result = validation.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateStart)
                .WithErrorMessage("Date start must be in the present.");
        }
        [Fact]
        public void End_date_is_in_the_present()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2025, 12, 31));

            GetDailyRoutineQueryParams queryParams =
                new(new DateOnly(2025, 1, 1), new DateOnly(2950, 1, 1), null);
            DailyRoutineQueryParamsValidation validation = new(timeProvider);

            var result = validation.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateEnd)
                .WithErrorMessage("Date end must be in the present.");
        }
    }
}
