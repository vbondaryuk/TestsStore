using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Application.Queries.TestResultQueries
{
	public interface ITestResultQueries
	{
		Task<TestResultViewModel> GetAsync(Guid id);

		Task<PaginatedItems<TestResultViewModel>> GetItemsAsync(
			Guid buildId, Status status, string filter, int pageSize, int pageIndex);

		Task<ICollection<TestResultViewModel>> GetByTestIdAsync(Guid testId, int count);
		
		Task<ICollection<TestResultsSummaryViewModel>> GetTestResultsSummaryAsync(Guid buildId);
	}
}