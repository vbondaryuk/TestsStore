using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;

namespace TestsStore.Api.Application.Commands.ProjectCommands
{
	public class ProjectCommandHandler : IProjectCommandHandler
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectCommandHandler(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public async Task<ICommandResult<Models.Project>> ExecuteAsync(CreateProjectCommand command)
		{
			if (string.IsNullOrWhiteSpace(command.Name))
			{
				return new CommandResult<Models.Project>(false, null);
			}
			var project = await _projectRepository.GetByName(command.Name);

			if (project == null)
			{
				project = new Models.Project(command.Name);
				await _projectRepository.Add(project);
			}

			return new CommandResult<Models.Project>(true, project);
		}
	}
}