using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public interface IBuildRepository
	{
		Task<Build> GetByIdAsync(Guid id);

		Task<ICollection<Build>> GetByProjectIdAsync(Guid projectId);

		Task<Build> AddAsync(Build build);

		Task<Build> UpdatedAsync(Build build);
	}
}