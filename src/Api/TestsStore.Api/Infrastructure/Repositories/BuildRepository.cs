using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public class BuildRepository : IBuildRepository
	{
		private readonly TestsStoreContext _testsStoreContext;

		public BuildRepository(TestsStoreContext testsStoreContext)
		{
			this._testsStoreContext = testsStoreContext;
		}

		public async Task<Build> GetById(Guid id)
		{
			var build = await _testsStoreContext.Builds
				.Include(x => x.Status)
				.FirstOrDefaultAsync(x => x.Id == id);

			return build;
		}

		public async Task<ICollection<Build>> GetByProjectId(Guid projectId)
		{
			var builds = await _testsStoreContext.Builds
				.Include(x => x.Status)
				.Where(x => x.ProjectId == projectId)
				.OrderByDescending(x => x.StartTime)
				.ToListAsync();

			return builds;
		}

		public async Task<Build> Add(Build build)
		{
			await _testsStoreContext.Builds.AddAsync(build);
			await _testsStoreContext.SaveChangesAsync();

			return build;
		}

		public async Task<Build> Updated(Build build)
		{
			_testsStoreContext.Builds.Update(build);
			await _testsStoreContext.SaveChangesAsync();

			return build;
		}
	}
}