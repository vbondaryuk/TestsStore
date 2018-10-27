using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public interface ITestRepository
	{
		Task<Test> GetById(Guid id);

		Task<ICollection<Test>> GetByProjectId(Guid projectId);

		Task<Test> Get(string name, string className, Guid projectId);

		Task<Test> Add(Test test);
	}
}