using Habits.API.DailyRoutines.DTO;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.DailyRoutineEndpoints
{
    [Collection("HabitsTests")]
    public class GetDailyRoutineTests(CustomWebApplication testApp)
    {
        //[Fact]
        //public async Task Get_all_daily_routines()
        //{
        //    const int TOTAL_MINUTES = 30;

        //    var client = authApp.CreateClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");

        //    var response = await client.GetAsync("https://localhost:7074/api/users/samu/dailyRoutines");
        //    response.EnsureSuccessStatusCode();
        //    GetAllDailyRoutinesResponse? json = await response.Content.ReadFromJsonAsync<GetAllDailyRoutinesResponse>();

        //    Assert.NotNull(json);
        //    //Assert.Equal(TOTAL_MINUTES, json.TotalMinutes);
        //    //Assert.Equal(1, json.Count);
        //}
        [Fact]
        public async Task Get_daily_routine()
        {
            var client = testApp.CreateAuthClient();

            var response = await client.GetAsync("https://localhost:7074/api/dailyRoutines/2");
            response.EnsureSuccessStatusCode();
            GetDailyRoutineResponse? json = await response.Content.ReadFromJsonAsync<GetDailyRoutineResponse>();

            Assert.NotNull(json);
            Assert.Equal(2, json.IdDailyRoutine);
        }
    }
}
