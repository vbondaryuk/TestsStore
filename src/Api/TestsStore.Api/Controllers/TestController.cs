using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Application.Queries.TestQueries;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : Controller
	{
		private readonly ITestQueries _testQueries;

		public TestController(ITestQueries testQueries)
		{
			_testQueries = testQueries;
		}

		// GET test/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var test = await _testQueries.GetAsync(id);

			if (test == null)
				return NotFound();

			return Ok(test);
		}

		// GET test/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var tests = await _testQueries.GetByProjectIdAsync(projectId);

			return Ok(tests);
		}
	}
}