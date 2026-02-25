using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Testing.IntegrationTesting
{
    public class DailyTasksTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        public DailyTasksTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task DailyTasksTodayNotReturnDailyTasksFromPreviousDays()
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            var client = _factory.CreateClient();

            var response = await client.GetAsync();
        }
    }
}
