using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestsStore.Api.Application.Queries.BuildQueries
{
	public interface IBuildQueries
	{
		Task<BuildViewModel> GetBuildAsync(Guid id);
		Task<ICollection<BuildViewModel>> GetByProjectIdAsync(Guid projectId);
	}
}