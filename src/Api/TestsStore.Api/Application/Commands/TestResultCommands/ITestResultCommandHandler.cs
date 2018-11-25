using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.TestResultCommands
{
	public interface ITestResultCommandHandler
		: ICommandHandler<CreateTestResultCommand, TestResult>,
			ICommandHandler<AddBatchTestResultCommand>
	{
	}
}