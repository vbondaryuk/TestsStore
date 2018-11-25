using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.BuildCommands
{
	public interface IBuildCommandHandler
		: ICommandHandler<CreateBuildCommand, Build>,
			ICommandHandler<UpdateBuildCommand, Build>
	{
	}
}