using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestsStore.Api.Application.Queries.TestQueries
{
	public interface ITestQueries
	{
		Task<TestViewModel> GetAsync(Guid id);
		Task<ICollection<TestViewModel>> GetByProjectIdAsync(Guid projectId);
	}
}