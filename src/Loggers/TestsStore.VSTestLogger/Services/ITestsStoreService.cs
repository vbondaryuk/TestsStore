using System;
using System.Threading.Tasks;
using TestsStore.VSTestLogger.Models;

namespace TestsStore.VSTestLogger.Services
{
	public interface ITestsStoreService
	{
		Task<Project> GetProjectAsync(string projectName);

		Task<Build> AddBuildAsync(Project project, Build build);
		Task UpdateBuildAsync(Build build);
		Task AddTestAsync(Project project, Build build, TestMethodResult testMethodResult);
	}
}