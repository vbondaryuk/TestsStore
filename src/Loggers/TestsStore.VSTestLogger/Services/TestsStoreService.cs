using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestsStore.VSTestLogger.Models;

namespace TestsStore.VSTestLogger.Services
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
			string response = await client.GetStringAsync($"projects/{projectName}").ConfigureAwait(false);
			
			return JsonConvert.DeserializeObject<Project>(response);
		}

		public async Task<Build> AddBuildAsync(Project project, Build build)
		{
			var byuildInfo = new {ProjectId = project.Id, Build = build};
			var buildContent = new StringContent(JsonConvert.SerializeObject(byuildInfo), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync("builds/", buildContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			return JsonConvert.DeserializeObject<Build>(jsonResponse);
		}

		public async Task UpdateBuildAsync(Build build)
		{
			var buildContent = new StringContent(JsonConvert.SerializeObject(build), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PutAsync("builds/", buildContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();
		}

		public async Task AddTestAsync(Project project, Build build, TestMethodResult testMethodResult)
		{
			var testInfo = new { ProjectId = project.Id, BuildId = build.Id, TestResult = testMethodResult };
			var testContent = new StringContent(JsonConvert.SerializeObject(testInfo), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync("tests/", testContent).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();
		}
	}
}