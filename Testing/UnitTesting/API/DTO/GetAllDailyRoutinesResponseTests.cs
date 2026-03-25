using Habits.API.DailyRoutines.DTO;
using Habits.Models;

namespace Testing.UnitTesting.API.DTO
{
    public class GetAllDailyRoutinesResponseTests
    {
        [Fact]
        public void Percentage_completed_is_0_when_no_daily_tasks()
        {
            List<DailyRoutine> tasks = [];

            var sut = tasks.ToGetAllDailyRoutinesResponse();

            Assert.Equal("00.00%", sut.PercentageCompleted);
        }
    }
}
