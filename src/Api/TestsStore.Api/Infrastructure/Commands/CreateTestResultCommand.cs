using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class CreateTestResultCommand : ICommand
	{
		public Guid ProjectId { get; set; }

		public Guid BuildId { get; set; }

		public string Name { get; set; }

		public string ClassName { get; set; }

		public TimeSpan Duration { get; set; }

		public string Status { get; set; }

		public string Messages { get; set; }

		public string StackTrace { get; set; }

		public string ErrorMessage { get; set; }
	}
}