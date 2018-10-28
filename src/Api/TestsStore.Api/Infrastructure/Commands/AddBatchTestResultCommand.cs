using System.Collections.Generic;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class AddBatchTestResultCommand : ICommand
	{
		public ICollection<CreateTestResultCommand> CreateTestResultCommands { get; set; }
	}
}