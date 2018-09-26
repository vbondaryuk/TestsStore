using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Infrastructure.Filters;

namespace TestsStore.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddCustomDbContext(Configuration)
				.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
				.AddControllersAsServices()
				.AddJsonOptions(options =>
					{
						options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSwagger();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.Enrich.WithProperty("TestStore.Api", "Serilog Web App");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			Log.Logger = logger.CreateLogger();
			loggerFactory.AddSerilog(Log.Logger);

			var pathBase = Configuration["PATH_BASE"];

			if (!string.IsNullOrEmpty(pathBase))
			{
				loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
				app.UsePathBase(pathBase);
			}

			app.UseCors("CorsPolicy");
			app.UseMvc();
			app.UseMvcWithDefaultRoute();
			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Catalog.API V1");
				});
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

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
				{
					Title = "Test store service API",
					Version = "v1",
					Description = "The test store service.",
					TermsOfService = "Terms Of Service"
				});
			});

			return services;

		}
	}
}
