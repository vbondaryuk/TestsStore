using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : Controller
	{
		private readonly ITestRepository _testRepository;

		public TestController(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		// GET test/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var test = await _testRepository.GetById(id);

			if (test == null)
				return NotFound();

			return Ok(test);
		}

		// GET test/project/guid
		[HttpGet]
		[Route("project/{projectId:Guid}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var tests = await _testRepository.GetByProjectId(projectId);

			return Ok(tests);
		}
	}
}