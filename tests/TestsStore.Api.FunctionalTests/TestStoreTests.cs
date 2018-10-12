using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsStore.Api.FunctionalTests.Utilities;

namespace TestsStore.Api.FunctionalTests
{
	[TestClass]
	public class TestStoreTests
	{
		private readonly WebApplicationFactory<Startup> factory;
		private readonly HttpClient client;

		public TestStoreTests()
		{
			factory = new CustomWebApplicationFactory<Startup>();
			client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[TestMethod]
		public async Task Get_IncorrectProjectId_Should_NotFound()
		{
			var response = await client.GetAsync($"api/project/id/{Guid.NewGuid()}");

			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		}

		[TestMethod]
		public async Task Get_Project_ById_Should_Ok()
		{
			var response = await client.GetAsync($"api/project/id/{SeedData.TestProject.Id}");

			response.EnsureSuccessStatusCode();
		}

		[TestMethod]
		public async Task Get_Projects_Should_Ok()
		{
			var response = await client.GetAsync($"api/test/project/{Guid.NewGuid()}");

			response.EnsureSuccessStatusCode();
		}
	}
}
