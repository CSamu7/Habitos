using Habits.API.Routines.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.RoutineEndpoints
{
    [Collection("Database collection")]
    public class RoutineIntegrationTests(CustomWebApplication application)
    {
        [Fact]
        public async void Get_routine()
        {
            var client = application.CreateAuthClient();
            var response = await client.GetAsync("https://localhost:7074/api/routines/2");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<GetRoutineResponse>();

            Assert.NotNull(json);
            Assert.Equal(120, json.Minutes);
            Assert.Equal(2, json.Id);
        }

        [Fact]
        public async void Get_all_routines()
        {
            var client = application.CreateAuthClient();
            var response = await client.GetAsync("https://localhost:7074/api/users/samu/routines/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<List<GetRoutineResponse>>();

            Assert.NotNull(json);
            Assert.Equal(2, json.Count);
        }

        [Fact]
        public async void Delete_routine()
        {
            var client = application.CreateAuthClient();
            var response = await client.DeleteAsync("https://localhost:7074/api/routines/2");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var responseGet = await client.GetAsync("https://localhost:7074/api/routines/2");
            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }
    }
}
