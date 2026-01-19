using Habits.Features.Tasks;
using System;
using System.Collections.Generic;
using Habits.Features.DailyTasks.Endpoints;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Time.Testing;

namespace UnitTesting
{
    public class GetAllDailyTaskFiltersTests
    {
        [Fact]
        public void Start_date_is_yesterday_if_not_given()
        {
            var faketime = new FakeTimeProvider(startDateTime: GetDate(2025, 01, 14));
            var filters = new GetAllDailyTaskFilters(null, new(2025, 01, 14), DailyTaskProgress.NotDone, faketime);
            DateTimeOffset start_date_expected = GetDate(2025, 01, 13);

            Assert.Equal(start_date_expected, filters.DateStart);
        }
        [Fact]
        public void End_date_is_now_if_not_given()
        {
            var faketime = new FakeTimeProvider(startDateTime: GetDate(2025, 01, 14));
            var filters = new GetAllDailyTaskFilters(null, new(2025, 01, 14), DailyTaskProgress.NotDone, faketime);
            
            DateTimeOffset end_date_expected = GetDate(2025, 01, 14);

            Assert.Equal(end_date_expected, filters.DateEnd);
        }
        [Fact]
        public void Neither_date_cant_be_in_the_future()
        {
            var faketime = new FakeTimeProvider();

            Assert.Throws<ArgumentException>(() =>
            {
                var filters = new GetAllDailyTaskFilters(new(2099, 01, 11), null, DailyTaskProgress.NotDone, faketime);
            });
        }
        [Fact]
        public void End_date_cant_be_less_than_start_date()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new GetAllDailyTaskFilters(new(2025, 01, 16), new(2025, 01, 14), DailyTaskProgress.NotDone, new FakeTimeProvider());
            });
        }
        private DateTimeOffset GetDate(int year, int month, int day) =>
            new(new DateTime(year, month, day), new(0));
    }
}
