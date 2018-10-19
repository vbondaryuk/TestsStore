using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.CommandModels;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

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
				.Include(x => x.Status)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (build == null)
			{
				return NotFound();
			}

			return Ok(build);
		}

		// GET build/id/guid/details
		[HttpGet]
		[Route("id/{id:Guid}/details")]
		public async Task<IActionResult> GetDetails(Guid id)//TODO remove should be skipped to build and testresult get summary
		{
			var build = await testsStoreContext.Builds
				.Include(x => x.Status)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (build == null)
				return NotFound();

			var testStatistic = await testsStoreContext.TestResults
				.Where(x => x.BuildId == id)
				.GroupBy(x => x.StatusId)
				.Select(x => new
				{
					StatusId = x.Key,
					Count = x.Count()
				}).ToListAsync();

			var testsSummaryViewModels = testStatistic.Select(x => new TestResultsSummaryViewModel
			{
				Status = Enumeration.FromValue<Status>(x.StatusId).Name,
				Count = x.Count
			}).ToList();

			var buildDetailsViewModel = new BuildDetailsViewModel(build, testsSummaryViewModels);

			return Ok(buildDetailsViewModel);
		}

		// GET build/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var builds = await testsStoreContext.Builds
				.Include(x => x.Status)
				.Where(x => x.ProjectId == projectId)
				.OrderByDescending(x => x.StartTime)
				.ToListAsync();

			return Ok(builds);
		}

		//Post build/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateBuild([FromBody]BuildCommandModel buildCommandModel)
		{
			Build build = await HandleAddCommandAsync(buildCommandModel);

			return CreatedAtAction(nameof(CreateBuild), build);
		}

		//Put build/items
		[HttpPut]
		[Route("items")]
		public async Task<IActionResult> UpdateBuild([FromBody]BuildCommandModel buildCommandModel)
		{
			Build build = await  HandleUpdateCommandAsync(buildCommandModel);

			return Ok(build);
		}

		private async Task<Build> HandleAddCommandAsync(BuildCommandModel buildCommandModel)
		{
			Build build = buildCommandModel.ToBuild();

			await testsStoreContext.Builds.AddAsync(build);
			await testsStoreContext.SaveChangesAsync();

			return build;
		}

		private async Task<Build> HandleUpdateCommandAsync(BuildCommandModel buildCommandModel)
		{
			var build = await testsStoreContext.Builds
				.FirstOrDefaultAsync(x => x.Id == buildCommandModel.Id);

			var status = Enumeration.FromDisplayName<Status>(buildCommandModel.Status);
			build.EndTime = buildCommandModel.EndTime;
			build.StatusId = status.Id;

			testsStoreContext.Builds.Update(build);
			await testsStoreContext.SaveChangesAsync();

			return build;
		}
	}
}