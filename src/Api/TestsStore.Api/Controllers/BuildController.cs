using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Model;
using TestsStore.Api.QueryModels;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	public class BuildController : Controller
	{
		private readonly TestsStoreContext testsStoreContext;

		public BuildController(TestsStoreContext testsStoreContext)
		{
			this.testsStoreContext = testsStoreContext ?? throw new ArgumentNullException(nameof(testsStoreContext));
		}

		// GET build/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var build = await testsStoreContext.Builds
				.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(build);
		}

		// GET build/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var builds = await testsStoreContext.Builds
				.Where(x => x.ProjectId == projectId)
				.ToListAsync();

			return Ok(builds);
		}

		//Post build/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateBuild([FromBody]BuildQueryModel buildQueryModel)
		{
			var build = QueryModelToBuild(buildQueryModel);

			var projectEntry = await testsStoreContext.Builds.AddAsync(build);
			await testsStoreContext.SaveChangesAsync();
			build = projectEntry.Entity;

			return Ok(build);
		}

		//Put build/items
		[HttpPut]
		[Route("items")]
		public async Task<IActionResult> UpdateBuild([FromBody]BuildQueryModel buildQueryModel)
		{
			var status = Enumeration.FromDisplayName<Status>(buildQueryModel.Status);

			var build = await testsStoreContext.Builds
				.FirstOrDefaultAsync(x => x.Id == buildQueryModel.Id);

			build.EndTime = buildQueryModel.EndTime;
			build.StatusId = status.Id;

			testsStoreContext.Builds.Update(build);
			await testsStoreContext.SaveChangesAsync();

			return Ok(build);
		}

		private static Build QueryModelToBuild(BuildQueryModel buildQueryModel)
		{
			var status = Enumeration.FromDisplayName<Status>(buildQueryModel.Status);
			var build = new Build
			{
				Id = Guid.NewGuid(),
				ProjectId = buildQueryModel.ProjectId,
				Name = buildQueryModel.Name,
				StatusId = status.Id,
				StartTime = buildQueryModel.StartTime,
				EndTime = buildQueryModel.EndTime
			};
			return build;
		}
	}
}