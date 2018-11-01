using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class CreateProjectCommand : ICommand
	{
		public CreateProjectCommand(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}

		public Guid Id { get; }

		public string Name { get; }
	}
}