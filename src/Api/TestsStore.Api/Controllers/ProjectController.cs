using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Application.Commands.ProjectCommands;
using TestsStore.Api.Application.Queries.ProjectQueries;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectCommandHandler _projectCommandHandler;
		private readonly IProjectQueries _projectQueries;

		public ProjectController(IProjectCommandHandler projectCommandHandler, IProjectQueries projectQueries)
		{
			_projectCommandHandler = projectCommandHandler;
			_projectQueries = projectQueries;
		}

		// GET project/items
		[HttpGet]
		[Route("items")]
		public async Task<IActionResult> Get()
		{
			var projects = await _projectQueries.GetAsync();

			return Ok(projects);
		}

		// GET project/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var project = await _projectQueries.GetAsync(id);

			if (project == null)
				return NotFound();

			return Ok(project);
		}

		// GET project/name/name
		[HttpGet]
		[Route("name/{name:minlength(1)}")]
		public async Task<IActionResult> Get(string name)
		{
			var project = await _projectQueries.GetAsync(name);

			if (project == null)
				return NotFound();

			return Ok(project);
		}

		//Post project/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand createProjectCommand)
		{
			var commandResult = await _projectCommandHandler.ExecuteAsync(createProjectCommand);

			if (commandResult.Success)
				return Ok(commandResult.Result);

			return BadRequest(createProjectCommand);
		}
	}
}