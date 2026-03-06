using Habits.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.IntegrationTesting
{
    public class CustomWebApplication : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public System.Threading.Tasks.Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault
                (d => d.ServiceType == typeof(DbContextOptions<HabitsContext>));

                services.Remove(dbContextDescriptor);
                
            });
        }

        System.Threading.Tasks.Task IAsyncLifetime.DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
