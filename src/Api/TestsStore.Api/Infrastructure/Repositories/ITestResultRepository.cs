using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public interface ITestResultRepository
	{
		Task<TestResult> Add(TestResult testResult);
		Task<ICollection<TestResult>> AddRange(ICollection<TestResult> testResults);
		Task<ICollection<(Status Status, int Count)>> GetSummary(Guid buildId);
		Task<TestResult> GetById(Guid id);
		Task<ICollection<TestResult>> GetByTestId(Guid testId, int count);
		Task<PaginatedItems<TestResult>> GetPaginatedItems(Guid buildId, string filter, Status status, int pageSize, int pageIndex);
	}
}