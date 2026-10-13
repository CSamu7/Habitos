
using Habits.API.Routines.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.RoutineEndpoints
{
    [Collection("Database collection")]
    public class PostRoutineTests(CustomWebApplication application) : IAsyncLifetime
    {
        [Fact]
        public async void Routine_is_less_than_eight_hours()
        {
            var body = new PostRoutineRequest("TestData", 8 * 60, []);
            var client = application.CreateAuthClient();
            var response = await client.PostAsJsonAsync("api/users/samu/routines/", body);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var body2 = new PostRoutineRequest("Prueba1", 1, []);
            var response2 = await client.PostAsJsonAsync("api/users/samu/routines/", body2);

            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async void Dont_create_non_existent_categories()
        {
            var body = new PostRoutineRequest("TestData", 30, [2, 3, 4, 5]);
            var client = application.CreateAuthClient();
            var response = await client.PostAsJsonAsync("api/users/samu/routines/", body);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var getNewRoutine = await client.GetAsync($"https://localhost:7074{response.Headers.Location}");
            getNewRoutine.EnsureSuccessStatusCode();
            var json = await getNewRoutine.Content.ReadFromJsonAsync<GetRoutineResponse>();

            Assert.NotNull(json);
            Assert.Equal(body.Minutes, json.Minutes);
            Assert.Equal(1, json.Id);
            Assert.Empty(json.Categories);
        }

        [Theory]
        [MemberData(nameof(SuccessfulPostRoutines))]
        public async void Post_routine(string name, int minutes, List<int> categories)
        {
            var body = new PostRoutineRequest(name, minutes, categories);
            var client = application.CreateAuthClient();
            var response = await client.PostAsJsonAsync("api/users/samu/routines/", body);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var getNewRoutine = await client.GetAsync($"https://localhost:7074{response.Headers.Location}");
            getNewRoutine.EnsureSuccessStatusCode();
            var json = await getNewRoutine.Content.ReadFromJsonAsync<GetRoutineResponse>();

            Assert.NotNull(json);
            Assert.Equal(minutes, json.Minutes);
            Assert.Equal(1, json.Id);
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public async Task DisposeAsync() => application.ResetDB();
        public static IEnumerable<object[]> SuccessfulPostRoutines =>
            new List<object[]>
            {
                new object[] {"TestData", 25, new List<int>() },
                new object[] { "TestData1", 30, new List<int>() { 1 } }
            };
    }
}
