using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.ProjectCommands
{
	public interface IProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
	{
	}
}