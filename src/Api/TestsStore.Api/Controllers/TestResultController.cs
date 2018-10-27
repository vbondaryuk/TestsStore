using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Commands;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestResultController : Controller
	{
		private readonly TestResultCommandHandler _testResultCommandHandler;
		private readonly ITestResultRepository _testResultRepository;

		public TestResultController(
			TestResultCommandHandler testResultCommandHandler,
			ITestResultRepository testResultRepository)

		{
			_testResultCommandHandler = testResultCommandHandler;
			_testResultRepository = testResultRepository;
		}

		// GET testresult/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<ActionResult<TestResult>> Get(Guid id)
		{
			var testResult = await _testResultRepository.GetById(id);
			if (testResult is null)
				return BadRequest();

			return Ok(testResult);
		}

		// GET testresult/items/build/guid[?filter=test&status=success&pageSize=10&pageIndex=1]
		[HttpGet]
		[Route("items/build/{buildId:Guid}")]
		public async Task<ActionResult<PaginatedItems<TestResult>>> GetItems(
			Guid buildId,
			[FromQuery] string filter,
			[FromQuery] string status,
			[FromQuery] int pageSize = 10,
			[FromQuery] int pageIndex = 0)
		{
			var statusObject = Enumeration.FromDisplayName<Status>(status);

			var paginatedItems =
				await _testResultRepository.GetPaginatedItems(buildId, filter, statusObject, pageSize, pageIndex);

			return Ok(paginatedItems);
		}

		// GET testresult/items/test/guid/statistic[?count=1]
		[HttpGet]
		[Route("items/test/{testId:Guid}/statistic")]
		public async Task<ActionResult<TestResult>> GetTestHistory(Guid testId, [FromQuery] int count = 10)
		{
			var testResults = await _testResultRepository.GetByTestId(testId, count);

			return Ok(testResults);
		}

		[HttpGet]
		[Route("testresult/summary/build/{buildId:Guid}")]
		private async Task<ActionResult<IEnumerable<TestResultsSummaryViewModel>>> GetTestsResultsSummary(Guid buildId)
		{
			var testStatistic = await _testResultRepository.GetSummary(buildId);

			var testResultsSummaryViewModels = testStatistic.Select(x => new TestResultsSummaryViewModel
			{
				Status = x.Status.Name,
				Count = x.Count
			}).ToList();

			return Ok(testResultsSummaryViewModels);
		}

		//Post testresult/items
		[HttpPost]
		[Route("items")]
		public async Task<ActionResult<IEnumerable<TestResult>>> CreateTestResult(
			[FromBody] CreateTestResultCommand createTestResultCommandModel)
		{
			var commandResult = await _testResultCommandHandler.ExecuteAsync(createTestResultCommandModel);

			if (commandResult.Success)
				return CreatedAtAction(nameof(CreateTestResult), commandResult.Result);

			return BadRequest(createTestResultCommandModel);
		}
	}
}