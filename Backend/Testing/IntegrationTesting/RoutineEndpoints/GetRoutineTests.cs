using Habits.API.Routines.DTO;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.RoutineEndpoints
{
    [Collection("Database collection")]
    public class GetRoutineTests(CustomWebApplication application) : IAsyncLifetime
    {
        [Fact]
        public async Task Get_routine()
        {

            CancellationToken token = CancellationToken.None;
            var client = application.CreateAuthClient();
            var body = new PostRoutineRequest("TestData", 8 * 60, []);

            var responsePost = await client.PostAsJsonAsync("api/users/samu/routines/", body, token);
            responsePost.EnsureSuccessStatusCode();

            var response = await client.GetAsync("/api/routines/1", token);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<GetRoutineResponse>(token);

            Assert.NotNull(json);
            Assert.Equal(8 * 60, json.Minutes);
            Assert.Equal(1, json.Id);
        }

        [Fact]
        public async Task Get_all_routines()
        {
            CancellationToken token = CancellationToken.None;

            var client = application.CreateAuthClient();
            var body = new PostRoutineRequest("TestData", 8 * 60, []);

            var responsePost = await client.PostAsJsonAsync("api/users/samu/routines/", body, token);
            responsePost.EnsureSuccessStatusCode();

            var response = await client.GetAsync("https://localhost:7074/api/users/samu/routines/", token);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<List<GetRoutineResponse>>(token);

            Assert.NotNull(json);
            Assert.Single(json);
        }
        public ValueTask InitializeAsync() => ValueTask.CompletedTask;
        public async ValueTask DisposeAsync() => await application.ResetDB();
    }
}
