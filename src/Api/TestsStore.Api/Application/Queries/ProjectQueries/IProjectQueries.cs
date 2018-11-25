using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestsStore.Api.Application.Queries.ProjectQueries
{
	public interface IProjectQueries
	{
		Task<ICollection<ProjectViewModel>> GetAsync();
		Task<ProjectViewModel> GetAsync(Guid id);
		Task<ProjectViewModel> GetAsync(string name);
	}
}