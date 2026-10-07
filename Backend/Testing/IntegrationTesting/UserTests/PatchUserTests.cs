using Habits.API.Users.DTO;
using System.Net;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.UserTests
{
    [Collection("Database collection")] 
    public class PatchUserTests(CustomWebApplication application)
    {
        [Fact]
        public async void User_cant_change_min_goal_more_than_twice_in_a_week()
        {
            var client = application.CreateAuthClient();

            var content = new PatchUserRequest(null, null, 77);
            var result = await client.PatchAsJsonAsync("api/users/", content);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            content = new PatchUserRequest(null, null, 55);
            result = await client.PatchAsJsonAsync("api/users/", content);
            var lastModification = await client.GetFromJsonAsync<GetUserResponse>("api/users/");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(lastModification);

            content = new PatchUserRequest(null, null, 66);
            result = await client.PatchAsJsonAsync("api/users/", content);

            Assert.NotNull(result);
            Assert.NotEqual(66, lastModification.MinGoal);
            Assert.Equal(HttpStatusCode.BadRequest,result.StatusCode);
        }

        [Fact]
        public async void CutOffTime_is_saved_in_UTC()
        {
            var client = application.CreateAuthClient();
           
            //12PM to 7PM
            var content = new PatchUserRequest("America/Mexico_City", 12, null);
            var result = await client.PatchAsJsonAsync("api/users/", content);

            DateTime time = new(2025, 3, 3, 12, 0, 0);

            var user = await client.GetFromJsonAsync<GetUserResponse>("api/users/");

            Assert.NotNull(user);
            Assert.Equal(time.ToUniversalTime().Hour, user.CutOffTime);
        }
    }
}
