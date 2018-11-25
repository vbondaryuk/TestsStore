using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Application.Queries.BuildQueries
{
	public class BuildQueries : IBuildQueries
	{
		private readonly IBuildRepository _buildRepository;

		public BuildQueries(IBuildRepository buildRepository)
		{
			_buildRepository = buildRepository;
		}

		public async Task<BuildViewModel> GetBuildAsync(Guid id)
		{
			var build = await _buildRepository.GetByIdAsync(id);

			return build == null ? null : Mapper.Map(build);
		}

		public async Task<ICollection<BuildViewModel>> GetByProjectIdAsync(Guid projectId)
		{
			var builds = await _buildRepository.GetByProjectIdAsync(projectId);
			
			return Mapper.Map(builds);
		}
	}
}