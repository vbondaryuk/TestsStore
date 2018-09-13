using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestsStore.Api.Infrastructure;

namespace TestsStore.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddCustomDbContext(Configuration)
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}

	public static class CustomExtensionMethods
	{
		public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TestsStoreContext>(options =>
			{
				options.UseSqlServer(configuration["ConnectionString"],
									 sqlServerOptionsAction: sqlOptions =>
									 {
										 sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
										 //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
										 sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
									 });

				// Changing default behavior when client evaluation occurs to throw. 
				// Default in EF Core would be to log a warning when client evaluation is performed.
				options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
				//Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
			});
			return services;
		}

	}
}
