using System.Collections.Generic;

namespace TestsStore.Api.Infrastructure.Commands
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