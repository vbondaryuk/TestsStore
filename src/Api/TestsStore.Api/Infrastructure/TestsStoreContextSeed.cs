using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Polly;
using TestsStore.Api.Model;

namespace TestsStore.Api.Infrastructure
{
	public class TestsStoreContextSeed
	{
		public async Task SeedAsync(TestsStoreContext context, IHostingEnvironment env, ILogger<TestsStoreContextSeed> logger)
		{
			var policy = CreatePolicy(logger, nameof(TestsStoreContextSeed));

			await policy.ExecuteAsync(async () =>
			{
				using (context)
				{
					context.Database.Migrate();

					if (!context.Statuses.Any())
					{
						context.Statuses.AddRange(Enumeration.GetAll<Status>());

						await context.SaveChangesAsync();
					}
				}
			});
		}

		private Policy CreatePolicy(ILogger<TestsStoreContextSeed> logger, string prefix, int retries = 3)
		{
			return Policy.Handle<SqlException>().
				WaitAndRetryAsync(
					retryCount: retries,
					sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
					}
				);
		}
	}
}