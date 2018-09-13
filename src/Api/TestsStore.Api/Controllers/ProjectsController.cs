using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Model;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly TestsStoreContext testsStoreContext;

		public ProjectsController(TestsStoreContext testsStoreContext)
		{
			this.testsStoreContext = testsStoreContext ?? throw new ArgumentNullException(nameof(testsStoreContext));
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var projects = await testsStoreContext.Projects.ToListAsync();

			return Ok(projects);
		}

		// GET projects/id/name
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<ActionResult> Get(Guid id)
		{
			var project = await testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(project);
		}

		// GET projects/name/name
		[HttpGet]
		[Route("name/{name:minlength(1)}")]
		public async Task<ActionResult> Get(string name)
		{
			var project = await testsStoreContext.Projects
				.FirstOrDefaultAsync(x => x.Name == name);

			if (project == null)
			{
				var projectEntry = await testsStoreContext.Projects.AddAsync(new Project(name));
				await testsStoreContext.SaveChangesAsync();
				project = projectEntry.Entity;
			}

			return Ok(project);
		}
	}
}