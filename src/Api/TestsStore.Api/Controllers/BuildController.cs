using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Application.Commands;
using TestsStore.Api.Application.Commands.BuildCommands;
using TestsStore.Api.Application.Queries.BuildQueries;
using TestsStore.Api.Models;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	public class BuildController : Controller
	{
		private readonly IBuildCommandHandler _buildCommandHandler;
		private readonly IBuildQueries _buildQueries;

		public BuildController(IBuildCommandHandler buildCommandHandler, IBuildQueries buildQueries)
		{
			_buildCommandHandler = buildCommandHandler;
			_buildQueries = buildQueries;
		}

		// GET build/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var build = await _buildQueries.GetBuildAsync(id);

			if (build == null)
				return NotFound();

			return Ok(build);
		}

		// GET build/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var builds = await _buildQueries.GetByProjectIdAsync(projectId);

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