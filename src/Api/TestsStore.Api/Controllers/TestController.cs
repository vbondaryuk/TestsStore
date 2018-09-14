using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Infrastructure;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : Controller
	{
		private readonly TestsStoreContext testsStoreContext;

		public TestController(TestsStoreContext testsStoreContext)
		{
			this.testsStoreContext = testsStoreContext ?? throw new ArgumentNullException(nameof(testsStoreContext));
		}

		// GET test/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var test = await testsStoreContext.Tests
				.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(test);
		}

		// GET test/id/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var tests = await testsStoreContext.Tests
				.Where(x => x.ProjectId == projectId)
				.ToListAsync();

			return Ok(tests);
		}
	}
}