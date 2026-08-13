using FluentValidation.TestHelper;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Validation;
using Microsoft.Extensions.Time.Testing;

namespace Testing.UnitTesting.API.Validations
{
    public class DailyRoutineQueryParamsValidationTests
    {
        [Fact]
        public void End_date_is_the_smallest()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2020, 10, 1));
            DateTimeOffset dateStart = new(new(2025, 1, 1));
            DateTimeOffset dateEnd = new(new(2024, 1, 1));

            GetDailyRoutineQueryParams queryParams = new(dateStart, dateEnd, []);
            DailyRoutineQueryParamsValidation sut = new(timeProvider);

            var result = sut.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateStart);
        }
        [Fact]
        public void Start_date_is_not_in_the_present()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2025, 12, 31));
            DateTimeOffset dateStart = new(new(2950, 1, 1));
            DateTimeOffset dateEnd = new(new(2024, 1, 1));

            GetDailyRoutineQueryParams queryParams = new(dateStart, dateEnd, []);
            DailyRoutineQueryParamsValidation validation = new(timeProvider);

            var result = validation.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateStart);
        }
        [Fact]
        public void End_date_is_not_in_the_present()
        {
            TimeProvider timeProvider = new FakeTimeProvider(startDateTime: new DateTime(2025, 12, 31));

            DateTimeOffset dateStart = new(new(2024, 1, 1));
            DateTimeOffset dateEnd = new(new(2950, 1, 1));

            GetDailyRoutineQueryParams queryParams = new(dateStart, dateEnd, []);
            DailyRoutineQueryParamsValidation sut = new(timeProvider);

            var result = sut.TestValidate(queryParams);

            result.ShouldHaveValidationErrorFor(x => x.DateEnd);
        }
    }
}
