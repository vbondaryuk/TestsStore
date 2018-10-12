using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Models;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly TestsStoreContext testsStoreContext;

		public ProjectController(TestsStoreContext testsStoreContext)
		{
			this.testsStoreContext = testsStoreContext ?? throw new ArgumentNullException(nameof(testsStoreContext));
		}

		// GET project/items
		[HttpGet]
		[Route("items")]
		public async Task<IActionResult> Get()
		{
			var projects = await testsStoreContext.Projects
				.OrderBy(x => x.Name)
				.ToListAsync();

			return Ok(projects);
		}

		// GET project/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var project = await testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Id == id);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(project);
		}

		// GET project/name/name
		[HttpGet]
		[Route("name/{name:minlength(1)}")]
		public async Task<IActionResult> Get(string name)
		{
			var project = await testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Name == name);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(project);
		}

		//Post project/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateProject([FromBody]Project projectForInsert)
		{
			var project = await testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Name == projectForInsert.Name);

			if (project == null)
			{
				project = new Project(projectForInsert.Name);
				var projectEntry = await testsStoreContext.Projects.AddAsync(project);
				await testsStoreContext.SaveChangesAsync();
				project = projectEntry.Entity;
			}

			return Ok(project);
		}
	}
}