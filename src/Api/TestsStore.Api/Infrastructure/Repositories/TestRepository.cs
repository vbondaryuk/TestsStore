using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public class TestRepository : ITestRepository
	{
		private readonly TestsStoreContext _testsStoreContext;

		public TestRepository(TestsStoreContext testsStoreContext)
		{
			_testsStoreContext = testsStoreContext;
		}

		public async Task<Test> GetById(Guid id)
		{
			var test = await _testsStoreContext.Tests
				.FirstOrDefaultAsync(x => x.Id == id);
			return test;
		}

		public async Task<ICollection<Test>> GetByProjectId(Guid projectId)
		{
			var tests = await _testsStoreContext.Tests
				.Where(x => x.ProjectId == projectId)
				.ToListAsync();

			return tests;
		}

		public async Task<Test> Get(string name, string className, Guid projectId)
		{
			var test = await _testsStoreContext.Tests
				.FirstOrDefaultAsync(x =>
					x.Name == name &&
					x.ClassName == className &&
					x.ProjectId == projectId);

			return test;
		}

		public async Task<Test> Add(Test test)
		{
			await _testsStoreContext.AddAsync(test);

			return test;
		}
	}
}