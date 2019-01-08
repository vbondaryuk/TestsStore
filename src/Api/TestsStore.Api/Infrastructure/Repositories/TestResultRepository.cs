using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Models;
using TestsStore.Api.ViewModels;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public class TestResultRepository : ITestResultRepository
	{
		private readonly TestsStoreContext _testsStoreContext;

		public TestResultRepository(TestsStoreContext testsStoreContext)
		{
			_testsStoreContext = testsStoreContext;
		}

		public async Task<TestResult> GetById(Guid id)
		{
			var testResult = await _testsStoreContext.TestResults
				.Include(x => x.Status)
				.Include(x => x.Test)
				.Include(x => x.Build)
				.FirstOrDefaultAsync(x => x.Id == id);

			return testResult;
		}

		//TODO probably better result is write an sql query
		public async Task<ICollection<(Status Status, int Count)>> GetSummary(Guid buildId)
		{
			var testSummary = await _testsStoreContext.TestResults
				.Where(x => x.BuildId == buildId)
				.GroupBy(x => x.StatusId)
				.Select(x => new
				{
					StatusId = x.Key,
					Count = x.Count()
				}).ToListAsync();

			return testSummary
				.Select(x => (Enumeration.FromValue<Status>(x.StatusId), x.Count))
				.ToList();
		}

		public async Task<PaginatedItems<TestResult>> GetPaginatedItems(
			Guid buildId,
			string filter,
			Status status,
			int pageSize,
			int pageIndex)
		{
			IQueryable<TestResult> query = _testsStoreContext.TestResults
				.Where(x => x.BuildId == buildId);

			if (!string.IsNullOrWhiteSpace(filter))
				query = query.Where(x =>
					x.Test.ClassName.Contains(filter) || x.Test.Name.Contains(filter) ||
					x.Status.Name.Contains(filter));

			if (status != null)
				query = query.Where(x => x.Status.Id == status.Id);

			var totalItems = await query.LongCountAsync();

			query = query.OrderBy(c => c.Test.ClassName)
				.ThenBy(x => x.Test.Name)
				.Include(x => x.Test)
				.Include(x => x.Status);

			query = query
				.Skip(pageSize * pageIndex)
				.Take(pageSize);

			var testResults = await query.ToListAsync();

			return new PaginatedItems<TestResult>(pageIndex, pageSize, totalItems, testResults);
		}

		public async Task<ICollection<TestResult>> GetByTestId(Guid testId, int count)
		{
			var testResults = await _testsStoreContext.TestResults
				.Include(x => x.Status)
				.Include(x => x.Test)
				.Include(x => x.Build)
				.Include(x => x.Build.Status)
				.Where(x => x.TestId == testId)
				.OrderByDescending(x => x.Build.StartTime)
				.Take(count)
				.ToListAsync();

			return testResults;
		}

		public async Task<TestResult> Add(TestResult testResult)
		{
			await _testsStoreContext.TestResults.AddAsync(testResult);
			await _testsStoreContext.SaveChangesAsync();

			return testResult;
		}

		public async Task<ICollection<TestResult>> AddRange(ICollection<TestResult> testResults)
		{
			await _testsStoreContext.TestResults.AddRangeAsync(testResults);
			await _testsStoreContext.SaveChangesAsync();

			return testResults;
		}
	}
}