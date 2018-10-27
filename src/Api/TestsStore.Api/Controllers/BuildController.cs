using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Commands;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	public class BuildController : Controller
	{
		private readonly BuildCommandHandler _buildCommandHandler;
		private readonly IBuildRepository _buildRepository;

		public BuildController(BuildCommandHandler buildCommandHandler, IBuildRepository buildRepository)
		{
			_buildCommandHandler = buildCommandHandler;
			_buildRepository = buildRepository;
		}

		// GET build/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var build = await _buildRepository.GetById(id);

			if (build == null)
				return NotFound();

			return Ok(build);
		}

		// GET build/id/guid/details
		//[HttpGet]
		//[Route("id/{id:Guid}/details")]
		//public async Task<IActionResult> GetDetails(Guid id)//TODO remove should be skipped to build and testresult get summary
		//{
		//	var build = await testsStoreContext.Builds
		//		.Include(x => x.Status)
		//		.FirstOrDefaultAsync(x => x.Id == id);

		//	if (build == null)
		//		return NotFound();

		//	var testStatistic = await testsStoreContext.TestResults
		//		.Where(x => x.BuildId == id)
		//		.GroupBy(x => x.StatusId)
		//		.Select(x => new
		//		{
		//			StatusId = x.Key,
		//			Count = x.Count()
		//		}).ToListAsync();

		//	var testsSummaryViewModels = testStatistic.Select(x => new TestResultsSummaryViewModel
		//	{
		//		Status = Enumeration.FromValue<Status>(x.StatusId).Name,
		//		Count = x.Count
		//	}).ToList();

		//	var buildDetailsViewModel = new BuildDetailsViewModel(build, testsSummaryViewModels);

		//	return Ok(buildDetailsViewModel);
		//}

		// GET build/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var builds = await _buildRepository.GetByProjectId(projectId);

			return Ok(builds);
		}

		//Post build/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateBuild([FromBody]CreateBuildCommand createBuildCommand)
		{

			ICommandResult<Build> commandResult = await _buildCommandHandler.ExecuteAsync(createBuildCommand);

			if (commandResult.Success)
				return CreatedAtAction(nameof(CreateBuild), commandResult.Result);

			return BadRequest(createBuildCommand);
		}

		//Put build/items
		[HttpPut]
		[Route("items")]
		public async Task<IActionResult> UpdateBuild([FromBody]UpdateBuildCommand updateBuildCommand)
		{
			ICommandResult<Build> commandResult = await _buildCommandHandler.ExecuteAsync(updateBuildCommand);

			if (commandResult.Success)
				return NoContent();

			return BadRequest(updateBuildCommand);
		}
	}
}