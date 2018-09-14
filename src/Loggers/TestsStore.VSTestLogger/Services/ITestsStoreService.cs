using System.Threading.Tasks;
using TestsStore.VS.TestLogger.Models;

namespace TestsStore.VS.TestLogger.Services
{
	public interface ITestsStoreService
	{
		Task<Project> GetProjectAsync(string projectName);

		Task<Build> AddBuildAsync(Build build);

		Task UpdateBuildAsync(Build build);

		Task AddTestAsync(TestMethodResult testMethodResult);
	}
}