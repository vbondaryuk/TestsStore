using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class ProjectCommandHandler
		: ICommandHandler<CreateProjectCommand, Project>
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectCommandHandler(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public async Task<ICommandResult<Project>> ExecuteAsync(CreateProjectCommand command)
		{
			var project = await _projectRepository.GetByName(command.Name);

			if (project == null)
			{
				project = new Project(command.Name);
				await _projectRepository.Add(project);
			}

			return new CommandResult<Project>(true, project);
		}
	}
}