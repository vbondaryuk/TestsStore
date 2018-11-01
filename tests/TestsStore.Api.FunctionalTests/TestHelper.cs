using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestsStore.Api.FunctionalTests.Utilities;
using TestsStore.Api.Infrastructure;

namespace TestsStore.Api.FunctionalTests
{
	public class CustomWebApplicationFactory<TStartup>
		: WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				// Create a new service provider.
				var serviceProvider = new ServiceCollection()
					.AddEntityFrameworkInMemoryDatabase()
					.BuildServiceProvider();

				// Add a database context (ApplicationDbContext) using an in-memory 
				// database for testing.
				services.AddDbContext<TestsStoreContext>(options =>
				{
					options.UseInMemoryDatabase("InMemoryDbForTesting");
					options.UseInternalServiceProvider(serviceProvider);
				});

				// Build the service provider.
				var sp = services.BuildServiceProvider();

				// Create a scope to obtain a reference to the database
				// context (ApplicationDbContext).
				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var context = scopedServices.GetRequiredService<TestsStoreContext>();
					var logger = scopedServices.GetRequiredService<ILogger<TStartup>>();

					// Ensure the database is created.
					context.Database.EnsureCreated();

					try
					{
						SeedData.FillDataBase(context);

						context.SaveChanges();
					}
					catch (Exception ex)
					{
						logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
					}
				}
			});
		}
	}
}