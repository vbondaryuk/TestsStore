using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly TestsStoreContext _testsStoreContext;

		public ProjectRepository(TestsStoreContext testsStoreContext)
		{
			_testsStoreContext = testsStoreContext;
		}

		public async Task<ICollection<Project>> GetAll()
		{
			var projects = await _testsStoreContext.Projects
				.OrderBy(x => x.Name)
				.ToListAsync();

			return projects;
		}

		public async Task<Project> GetById(Guid id)
		{
			var project = await _testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Id == id);

			return project;
		}

		public async Task<Project> GetByName(string name)
		{
			var project = await _testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Name == name);

			return project;
		}

		public async Task<Project> Add(Project project)
		{
			await _testsStoreContext.Projects.AddAsync(project);
			await _testsStoreContext.SaveChangesAsync();

			return project;
		}
	}
}