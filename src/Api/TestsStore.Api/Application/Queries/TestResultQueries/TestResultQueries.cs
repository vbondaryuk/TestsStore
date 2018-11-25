using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Application.Queries.TestResultQueries
{
	public class TestResultQueries : ITestResultQueries
	{
		private readonly ITestResultRepository _testResultRepository;

		public TestResultQueries(ITestResultRepository testResultRepository)
		{
			_testResultRepository = testResultRepository;
		}

		public async Task<TestResultViewModel> GetAsync(Guid id)
		{
			var testResult = await _testResultRepository.GetById(id);

			return testResult is null ? null : Mapper.Map(testResult);
		}

		public async Task<PaginatedItems<TestResultViewModel>> GetItemsAsync(
			Guid buildId, Status status, string filter, int pageSize, int pageIndex)
		{
			var paginatedItems =
				await _testResultRepository.GetPaginatedItems(buildId, filter, status, pageSize, pageIndex);

			var paginatedViewModel = new PaginatedItems<TestResultViewModel>(
				paginatedItems.PageIndex, paginatedItems.PageSize, paginatedItems.Count,
				Mapper.Map(paginatedItems.Data));

			return paginatedViewModel;
		}

		public async Task<ICollection<TestResultViewModel>> GetByTestIdAsync(Guid testId, int count)
		{
			var testResults = await _testResultRepository.GetByTestId(testId, count);

			return Mapper.Map(testResults);
		}

		public async Task<ICollection<TestResultsSummaryViewModel>> GetTestResultsSummaryAsync(Guid buildId)
		{
			var testResultSummary = await _testResultRepository.GetSummary(buildId);

			var testResultsSummaryViewModels = testResultSummary.Select(x => new TestResultsSummaryViewModel
			{
				Status = x.Status.Name,
				Count = x.Count
			}).ToList();

			return testResultsSummaryViewModels;
		}
	}
}