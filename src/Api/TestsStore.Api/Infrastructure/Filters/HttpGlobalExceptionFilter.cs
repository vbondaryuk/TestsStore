﻿using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TestsStore.Api.Infrastructure.ActionResults;

namespace TestsStore.Api.Infrastructure.Filters
{
	public class HttpGlobalExceptionFilter : IExceptionFilter
	{
		private readonly IHostingEnvironment env;
		private readonly ILogger<HttpGlobalExceptionFilter> logger;

		public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
		{
			this.env = env;
			this.logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			logger.LogError(new EventId(context.Exception.HResult),
				context.Exception,
				context.Exception.Message);

			var json = new JsonErrorResponse
			{
				Messages = new[] { "An error ocurred." }
			};

			if (env.IsDevelopment())
			{
				json.DeveloperMeesage = context.Exception;
			}

			context.Result = new InternalServerErrorObjectResult(json);
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.ExceptionHandled = true;
		}

		private class JsonErrorResponse
		{
			public string[] Messages { get; set; }

			public object DeveloperMeesage { get; set; }
		}
	}
}