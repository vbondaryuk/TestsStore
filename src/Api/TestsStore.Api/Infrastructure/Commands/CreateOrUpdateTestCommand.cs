using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class CreateOrUpdateTestCommand : ICommand
	{
		public string Name { get; set; }

		public string ClassName { get; set; }

		public Guid ProjectId { get; set; }
	}
}