using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.CommandModels;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

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
		public async Task<ActionResult<TestResult>> Get(Guid id)
		{
			var testResult = await testsStoreContext.TestResults
				.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(testResult);
		}

		// GET testresult/items/build/guid[?filter=test&pageSize=10&pageIndex=1]
		[HttpGet]
		[Route("items/build/{buildId:Guid}")]
		public async Task<ActionResult<PaginatedItemsViewModel<TestResult>>> GetItems(Guid buildId, [FromQuery]string filter, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
		{
			var query = testsStoreContext.TestResults
				.Where(x => x.BuildId == buildId);

			if (!string.IsNullOrWhiteSpace(filter))
			{
				query = query.Where(x =>
					x.Test.ClassName.Contains(filter) || x.Test.Name.Contains(filter) ||
					x.Status.Name.Contains(filter));
			}

			query = query.OrderBy(c => c.Test.ClassName)
				.ThenBy(x => x.Test.Name)
				.Include(x => x.Test)
				.Include(x => x.Status);

			var totalItems = await query.LongCountAsync();

			query = query
				.Skip(pageSize * pageIndex)
				.Take(pageSize);

			var testResults = await query.ToListAsync();
			var model = new PaginatedItemsViewModel<TestResult>(pageIndex, pageSize, totalItems, testResults);
			
			return Ok(model);
		}

		// GET testresult/items/test/guid/statistic[?count=1]
		[HttpGet]
		[Route("items/test/{testId:Guid}/statistic")]
		public async Task<ActionResult<TestResult>> GetTestStatistics(Guid testId, [FromQuery]int count = 10)
		{
			var testResults = await testsStoreContext.TestResults
				.Include(x => x.Status)
				.Include(x => x.Test)
				.Include(x => x.Build)
				.Where(x => x.TestId == testId)
				.OrderByDescending(x => x.Build.StartTime)
				.Take(count)
				.ToListAsync();

			return Ok(testResults);
		}

		[HttpGet]
		[Route("testresult/summary/build/{buildId:Guid}")]
		private async Task<ActionResult<IEnumerable<TestResultsSummaryViewModel>>> GetTestsResultsSummary(Guid buildId)
		{
			var testStatistic = await testsStoreContext.TestResults
				.Where(x => x.BuildId == buildId)
				.GroupBy(x => x.StatusId)
				.Select(x => new
				{
					StatusId = x.Key,
					Count = x.Count()
				}).ToListAsync();

			var testResultsSummaryViewModels = testStatistic.Select(x => new TestResultsSummaryViewModel
			{
				Status = Enumeration.FromValue<Status>(x.StatusId).Name,
				Count = x.Count
			}).ToList();

			return Ok(testResultsSummaryViewModels);
		}

		//Post testresult/items
		[HttpPost]
		[Route("items")]
		public async Task<ActionResult<IEnumerable<TestResult>>> CreateTestResult([FromBody]TestResultCommandModel testResultCommandModel)
		{
			var testResultForInsert = await HandleAddCommand(testResultCommandModel);

			var projectEntry = await testsStoreContext.TestResults.AddAsync(testResultForInsert);
			await testsStoreContext.SaveChangesAsync();
			var testResult = projectEntry.Entity;

			return Ok(testResult);
		}

		private async Task<TestResult> HandleAddCommand(TestResultCommandModel testResultQueryModel)
		{
			var status = Enumeration.FromDisplayName<Status>(testResultQueryModel.Status);
			var test = await GetOrCreateTestAsync(testResultQueryModel);

			return new TestResult
			{
				Id = Guid.NewGuid(),
				TestId = test.Id,
				BuildId = testResultQueryModel.BuildId,
				Duration = testResultQueryModel.Duration.Milliseconds,
				StatusId = status.Id,
				Messages = testResultQueryModel.Messages,
				StackTrace = testResultQueryModel.StackTrace,
				ErrorMessage = testResultQueryModel.ErrorMessage
			};
		}

		private async Task<Test> GetOrCreateTestAsync(TestResultCommandModel testResultQueryModel)
		{
			var test = await testsStoreContext.Tests
				.FirstOrDefaultAsync(x =>
					x.Name == testResultQueryModel.Name && 
					x.ClassName == testResultQueryModel.ClassName &&
					x.ProjectId == testResultQueryModel.ProjectId);

			if (test == null)
			{
				test = new Test
				{
					Id = Guid.NewGuid(),
					Name = testResultQueryModel.Name,
					ClassName = testResultQueryModel.ClassName,
					ProjectId = testResultQueryModel.ProjectId
				};
				await testsStoreContext.Tests.AddAsync(test);
			}

			return test;
		}
	}
}