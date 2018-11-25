using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Application.Queries.ProjectQueries
{
	public class ProjectQueries : IProjectQueries
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectQueries(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}
		
		public async Task<ICollection<ProjectViewModel>> GetAsync()
		{
			var projects = await _projectRepository.GetAll();
			
			return Mapper.Map(projects);
		}

		public async Task<ProjectViewModel> GetAsync(Guid id)
		{
			var project = await _projectRepository.GetById(id);

			return project == null ? null : Mapper.Map(project);
		}

		public async Task<ProjectViewModel> GetAsync(string name)
		{
			var project = await _projectRepository.GetByName(name);

			return project == null ? null : Mapper.Map(project);
		}
	}
}