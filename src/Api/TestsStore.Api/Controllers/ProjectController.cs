using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Commands;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly ProjectCommandHandler _projectCommandHandler;
		private readonly IProjectRepository _projectRepository;

		public ProjectController(ProjectCommandHandler projectCommandHandler, IProjectRepository projectRepository)
		{
			_projectCommandHandler = projectCommandHandler;
			_projectRepository = projectRepository;
		}

		// GET project/items
		[HttpGet]
		[Route("items")]
		public async Task<IActionResult> Get()
		{
			var projects = await _projectRepository.GetAll();

			return Ok(projects);
		}

		// GET project/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var project = await _projectRepository.GetById(id);

			if (project == null)
				return NotFound();

			return Ok(project);
		}

		// GET project/name/name
		[HttpGet]
		[Route("name/{name:minlength(1)}")]
		public async Task<IActionResult> Get(string name)
		{
			var project = await _projectRepository.GetByName(name);

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