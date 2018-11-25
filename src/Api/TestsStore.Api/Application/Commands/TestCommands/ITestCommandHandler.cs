using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.TestCommands
{
	public interface ITestCommandHandler : ICommandHandler<CreateOrUpdateTestCommand, Test>
	{
	}
}