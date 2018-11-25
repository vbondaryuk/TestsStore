using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Application.Queries.TestQueries
{
	public class TestQueries : ITestQueries
	{
		private readonly ITestRepository _testRepository;

		public TestQueries(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		public async Task<TestViewModel> GetAsync(Guid id)
		{
			var test = await _testRepository.GetById(id);

			return test == null ? null : Mapper.Map(test);
		}

		public async Task<ICollection<TestViewModel>> GetByProjectIdAsync(Guid projectId)
		{
			var tests = await _testRepository.GetByProjectId(projectId);

			return Mapper.Map(tests);
		}
	}
}