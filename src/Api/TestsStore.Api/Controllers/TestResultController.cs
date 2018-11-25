using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Application.Commands.TestResultCommands;
using TestsStore.Api.Application.Queries.TestQueries;
using TestsStore.Api.Application.Queries.TestResultQueries;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestResultController : Controller
	{
		private readonly ITestResultCommandHandler _testResultCommandHandler;
		private readonly ITestResultQueries _testResultQueries;

		public TestResultController(
			ITestResultCommandHandler testResultCommandHandler,
			ITestResultQueries testResultQueries)

		{
			_testResultCommandHandler = testResultCommandHandler;
			_testResultQueries = testResultQueries;
		}

		// GET testresult/id/guid
		[HttpGet]
		[Route("id/{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var testResult = await _testResultQueries.GetAsync(id);
			if (testResult is null)
				return BadRequest();

			return Ok(testResult);
		}

		// GET testresult/items/build/guid[?filter=test&status=success&pageSize=10&pageIndex=1]
		[HttpGet]
		[Route("items/build/{buildId:Guid}")]
		public async Task<IActionResult> GetItems(
			Guid buildId,
			[FromQuery] string filter,
			[FromQuery] string status,
			[FromQuery] int pageSize = 10,
			[FromQuery] int pageIndex = 0)
		{
			Status statusObject = status is null ? null : Enumeration.FromDisplayName<Status>(status);

			var paginatedItems =
				await _testResultQueries.GetItemsAsync(buildId, statusObject, filter, pageSize, pageIndex);

			return Ok(paginatedItems);
		}

		// GET testresult/items/test/guid/statistic[?count=1]
		[HttpGet]
		[Route("items/test/{testId:Guid}/statistic")]
		public async Task<IActionResult> GetItemsByTestId(Guid testId, [FromQuery] int count = 10)
		{
			var testResults = await _testResultQueries.GetByTestIdAsync(testId, count);

			return Ok(testResults);
		}

		//GET testresult/summary/build/{buildId:Guid}
		[HttpGet]
		[Route("summary/build/{buildId:Guid}")]
		public async Task<IActionResult> GetTestsResultsSummary(Guid buildId)
		{
			var testResultSummary = await _testResultQueries.GetTestResultsSummaryAsync(buildId);

			var testResultsSummaryViewModels = testResultSummary.Select(x => new TestResultsSummaryViewModel
			{
				Status = x.Status,
				Count = x.Count
			}).ToList();

			return Ok(testResultsSummaryViewModels);
		}

		//Post testresult/items
		[HttpPost]
		[Route("items")]
		public async Task<IActionResult> CreateTestResult(
			[FromBody] CreateTestResultCommand createTestResultCommandModel)
		{
			var commandResult = await _testResultCommandHandler.ExecuteAsync(createTestResultCommandModel);

			if (commandResult.Success)
				return CreatedAtAction(nameof(CreateTestResult), commandResult.Result);

			return BadRequest(createTestResultCommandModel);
		}
	}
}