using Habits.API.DailyRoutines.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.DailyRoutineEndpoints
{
    [Collection("Database collection")]
    public class GetDailyRoutineTests(CustomWebApplication testApp)
    {
        [Fact]
        public async Task Get_all_daily_routines()
        {
            var client = testApp.CreateAuthClient();
            var response = await client.GetAsync("https://localhost:7074/api/users/samu/dailyroutines?progress=Incomplete&dateStart=2026/01/01");
            response.EnsureSuccessStatusCode();
            GetAllDailyRoutinesResponse? json = await response.Content.ReadFromJsonAsync<GetAllDailyRoutinesResponse>();

            Assert.NotNull(json);
            Assert.Equal(1, json.Count);
        }
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

        [Fact]
        public async Task Get_404_if_user_is_not_auth()
        {
            var client = testApp.CreateClient();
            var response = await client.GetAsync("https://localhost:7074/api/dailyRoutines/2");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
