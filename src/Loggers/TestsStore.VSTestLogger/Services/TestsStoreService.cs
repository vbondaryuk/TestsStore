using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestsStore.VS.TestLogger.Models;

namespace TestsStore.VS.TestLogger.Services
{
	public class TestsStoreService : ITestsStoreService
	{
		private readonly HttpClient client;

		public TestsStoreService(string testsStoreApiUrl)
		{
			client = new HttpClient {BaseAddress = new Uri($"{testsStoreApiUrl}/api/")};
		}

		public async Task<Project> GetProjectAsync(string projectName)
		{
			Project project = null;
			var httpResponseMessage = await client.GetAsync($"project/name/{projectName}").ConfigureAwait(false);
			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
				{
					var projectInfo = new { Name = projectName };
					var buildContent = new StringContent(JsonConvert.SerializeObject(projectInfo), System.Text.Encoding.UTF8, "application/json");
					var response = await client.PostAsync("project/items", buildContent).ConfigureAwait(false);

					response.EnsureSuccessStatusCode();

					var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					project = JsonConvert.DeserializeObject<Project>(responseJson);
				}
			}
			else
			{
				string responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
				project = JsonConvert.DeserializeObject<Project>(responseJson);
			}

			return project;
		}

		public async Task<Build> AddBuildAsync(Build build)
		{
			var buildContent = new StringContent(JsonConvert.SerializeObject(build), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync("build/items", buildContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			return JsonConvert.DeserializeObject<Build>(jsonResponse);
		}

		public async Task UpdateBuildAsync(Build build)
		{
			var buildContent = new StringContent(JsonConvert.SerializeObject(build), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PutAsync("build/items", buildContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();
		}

		public async Task AddTestAsync(TestMethodResult testMethodResult)
		{
			var testContent = new StringContent(JsonConvert.SerializeObject(testMethodResult), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync("testresult/items", testContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();
		}
	}
}