using Habits.API.DailyRoutines.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
            var client = testApp.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>
                            ("TestScheme", _ => { });

                    services.Configure<AuthenticationOptions>(options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    });
                });
            }).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");

            var response = await client.GetAsync("https://localhost:7074/api/dailyRoutines/2");
            var aa = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            GetDailyRoutineResponse? json = await response.Content.ReadFromJsonAsync<GetDailyRoutineResponse>();

            Assert.NotNull(json);
            Assert.Equal(2, json.IdDailyRoutine);
        }
    }
}
