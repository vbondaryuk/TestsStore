using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestsStore.Api.Infrastructure;

namespace TestsStore.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args)
				.Build()
				.MigrateDbContext<TestsStoreContext>((context, services) =>
				{
					var env = services.GetService<IHostingEnvironment>();
					var logger = services.GetService<ILogger<TestsStoreContextSeed>>();

					new TestsStoreContextSeed()
						.SeedAsync(context, env, logger)
						.Wait();
				})
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureLogging((hostingContext, builder) =>
				{
					builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					builder.AddConsole();
					builder.AddDebug();
				});
	}
}
