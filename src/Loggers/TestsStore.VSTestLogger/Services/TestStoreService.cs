using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestsStore.VSTestLogger.Models;

namespace TestsStore.VSTestLogger.Services
{
	public class TestStoreService : ITestStoreService
	{
		private readonly HttpClient client;

		public TestStoreService()
		{
			client = new HttpClient {BaseAddress = new Uri("/api/")};
		}

		public async Task<Project> GetProjectIdAsync(string projectName)
		{
			string response = await client.GetStringAsync($"projects/{projectName}");
			
			return JsonConvert.DeserializeObject<Project>(response);
		}

		public async Task<Build> CreateBuildAsync(Guid projectId, string buildName)
		{
			var byuildInfo = new {ProjectId = projectId, BuildName = buildName};
			var buildContent = new StringContent(JsonConvert.SerializeObject(byuildInfo), System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync("builds/", buildContent);

			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<Build>(jsonResponse);
		}
	}
}