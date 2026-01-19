using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    public class DailyTasksEndpointTests
    {
        [Fact(Skip ="Hitting Database. Looking for another alternative")]
        public async Task GetDailyTasks()
        {
            int idUser = 1;
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var result = await client.GetAsync($"/{idUser}/dailyTasks/today");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode); 
        }
    }
}
