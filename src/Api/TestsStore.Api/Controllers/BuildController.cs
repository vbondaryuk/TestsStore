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