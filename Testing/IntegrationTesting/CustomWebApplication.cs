using DotNet.Testcontainers.Builders;
using Habits.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Testing.IntegrationTesting
{
    [CollectionDefinition("Database collection")]
    public class DbSharedTestCollection : ICollectionFixture<CustomWebApplication> { }
    public class CustomWebApplication : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder
            ("habits-test-database:0.1")
            .WithName("test")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("All done!"))
            .Build();
        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<HabitsContext>();

            dbContext.Database.EnsureCreated();
        }
        public new async Task DisposeAsync()
        {
            await _sqlContainer.DisposeAsync();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                string connectionString = _sqlContainer.GetConnectionString();
                SqlConnectionStringBuilder builder = new(connectionString);

                builder.InitialCatalog = "Habits";

                var dbContextDescriptor = services.SingleOrDefault
                (d => d.ServiceType == typeof(DbContextOptions<HabitsContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<HabitsContext>((_, option) => option.UseSqlServer(builder.ToString()));
            });
        }
    }
}
