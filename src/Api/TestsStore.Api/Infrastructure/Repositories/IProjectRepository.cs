using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Repositories
{
	public interface IProjectRepository
	{
		Task<Project> Add(Project project);

		Task<ICollection<Project>> GetAll();

		Task<Project> GetById(Guid id);

		Task<Project> GetByName(string name);
	}
}