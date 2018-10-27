using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public interface IBuildRepository
	{
		Task<Build> GetById(Guid id);

		Task<ICollection<Build>> GetByProjectId(Guid projectId);

		Task<Build> Add(Build build);

		Task<Build> Updated(Build build);
	}
}