using System.Collections.Generic;

namespace TestsStore.Api.Application.Commands.TestResultCommands
{
	public class AddBatchTestResultCommand : ICommand
	{
		public AddBatchTestResultCommand(ICollection<CreateTestResultCommand> testResultCommands)
		{
			CreateTestResultCommands = testResultCommands;
		}

		public ICollection<CreateTestResultCommand> CreateTestResultCommands { get; }
	}
}