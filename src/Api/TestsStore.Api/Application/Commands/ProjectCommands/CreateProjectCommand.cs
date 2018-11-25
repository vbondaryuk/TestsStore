using System;

namespace TestsStore.Api.Application.Commands.ProjectCommands
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