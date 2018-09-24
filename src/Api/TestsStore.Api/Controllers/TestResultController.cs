using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.CommandModels;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Models;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestResultController : Controller
	{
		private readonly TestsStoreContext testsStoreContext;

		public TestResultController(TestsStoreContext testsStoreContext)
		{
			this.testsStoreContext = testsStoreContext ?? throw new ArgumentNullException(nameof(testsStoreContext));
		}

		// GET testresult/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var testResult = await testsStoreContext.TestResults
				.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(testResult);
		}

		// GET testresult/items/build/guid
		[HttpGet]
		[Route("items/build/{buildId:Guid}")]
		public async Task<IActionResult> GetItems(Guid buildId)
		{
			var testResults = await testsStoreContext.TestResults
				.Include(x => x.Test)
				.Include(x => x.Status)
				.Where(x => x.BuildId == buildId)
				.OrderBy(x => x.Test.ClassName)
				.ThenBy(x => x.Test.Name)
				.ToListAsync();

			return Ok(testResults);
		}

		//Post testresult/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateTestResult([FromBody]TestReslutCommandModel testReslutCommandModel)
		{
			var testResultForInsert = await HandleAddCommand(testReslutCommandModel);

			var projectEntry = await testsStoreContext.TestResults.AddAsync(testResultForInsert);
			await testsStoreContext.SaveChangesAsync();
			var testResult = projectEntry.Entity;

			return Ok(testResult);
		}

		private async Task<TestResult> HandleAddCommand(TestReslutCommandModel testReslutQueryModel)
		{
			var status = Enumeration.FromDisplayName<Status>(testReslutQueryModel.Status);
			var test = await GetOrCreateTestAsync(testReslutQueryModel);

			return new TestResult
			{
				Id = Guid.NewGuid(),
				TestId = test.Id,
				BuildId = testReslutQueryModel.BuildId,
				Duration = testReslutQueryModel.Duration,
				StatusId = status.Id,
				Messages = testReslutQueryModel.Messages,
				StackTrace = testReslutQueryModel.StackTrace,
				ErrorMessage = testReslutQueryModel.ErrorMessage
			};
		}

		private async Task<Test> GetOrCreateTestAsync(TestReslutCommandModel testReslutQueryModel)
		{
			var test = await testsStoreContext.Tests
				.FirstOrDefaultAsync(x => x.Name == testReslutQueryModel.Name && x.ClassName == testReslutQueryModel.ClassName);

			if (test == null)
			{
				test = new Test
				{
					Id = Guid.NewGuid(),
					Name = testReslutQueryModel.Name,
					ClassName = testReslutQueryModel.ClassName,
					ProjectId = testReslutQueryModel.ProjectId
				};
				await testsStoreContext.Tests.AddAsync(test);
			}

			return test;
		}
	}
}