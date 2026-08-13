using DotNet.Testcontainers.Builders;
using Habits.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace Testing.IntegrationTesting
{
    [CollectionDefinition("Database collection")]
    public class DbSharedTestCollection : ICollectionFixture<CustomWebApplication> { }
    public class CustomWebApplication : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder
            ("a:1.0")
            .WithName("habits-integration-tests")
            .WithExposedPort(1433)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("All done!"))
            .Build();

        private SqlConnection _dbConnection = null!;
        private Respawner _respawner = null!;
        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();
            _dbConnection = new(_sqlContainer.GetConnectionString());

            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<HabitsContext>();
            dbContext.Database.EnsureCreated();

            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection);
        }

        public async void ResetDB() {
            await _respawner.ResetAsync(_dbConnection);
        }
        public new async Task DisposeAsync()
        {
            await _sqlContainer.DisposeAsync();
            await _dbConnection.DisposeAsync();
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
