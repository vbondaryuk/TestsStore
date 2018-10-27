using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class CreateProjectCommand : ICommand
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Name { get; set; }
	}
}