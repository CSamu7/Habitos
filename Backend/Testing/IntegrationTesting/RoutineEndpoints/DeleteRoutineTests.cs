using Habits.API.Routines.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.RoutineEndpoints
{
    [Collection("Database collection")]
    public class DeleteRoutineTests(CustomWebApplication application) : IAsyncLifetime
    {
        [Fact]
        public async void Delete_routine_is_successful() 
        {
            var body = new PostRoutineRequest("TestData", 8 * 2, []);
            var client = application.CreateAuthClient();
            var response = await client.PostAsJsonAsync("api/users/samu/routines/", body);

            var response2 = await client.DeleteAsync(response.Headers.Location);
            Assert.Equal(HttpStatusCode.NoContent, response2.StatusCode);

            var response3 = await client.GetAsync(response.Headers.Location);
            Assert.Equal(HttpStatusCode.NotFound, response3.StatusCode);
        }
        public Task InitializeAsync() => Task.CompletedTask;
        public async Task DisposeAsync() => application.ResetDB();

    }
}
