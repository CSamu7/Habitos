using Habits.API.Users.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.UserTests
{
    [Collection("Database collection")] 
    public class PatchUserTests(CustomWebApplication application) : IAsyncLifetime
    {
        [Fact]
        public async Task User_cant_change_min_goal_more_than_twice_in_a_week()
        {
            CancellationToken token = CancellationToken.None;
            var client = application.CreateAuthClient();

            var content = new PatchUserRequest(null, null, 77);
            var result = await client.PatchAsJsonAsync("api/users/", content, token);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            content = new PatchUserRequest(null, null, 55);
            result = await client.PatchAsJsonAsync("api/users/", content, token);
            var lastModification = await client.GetFromJsonAsync<GetUserResponse>("api/users/", token);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(lastModification);

            content = new PatchUserRequest(null, null, 66);
            result = await client.PatchAsJsonAsync("api/users/", content, token);

            Assert.NotNull(result);
            Assert.NotEqual(66, lastModification.MinGoal);
            Assert.Equal(HttpStatusCode.BadRequest,result.StatusCode);
        }

        [Fact]
        public async Task CutOffTime_is_saved_in_UTC()
        {
            CancellationToken token = CancellationToken.None;
            var client = application.CreateAuthClient();
           
            //12PM to 7PM
            var content = new PatchUserRequest("America/Mexico_City", 12, null);
            var result = await client.PatchAsJsonAsync("api/users/", content, token);

            DateTime time = new(2025, 3, 3, 12, 0, 0);

            var user = await client.GetFromJsonAsync<GetUserResponse>("api/users/", token);

            Assert.NotNull(user);
            Assert.Equal(time.ToUniversalTime().Hour, user.CutOffTime);
        }

        public ValueTask InitializeAsync() => ValueTask.CompletedTask;
        public async ValueTask DisposeAsync() => await application.ResetDB();
    }
}
