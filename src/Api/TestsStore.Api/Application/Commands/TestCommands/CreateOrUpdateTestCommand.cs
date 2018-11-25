using System;

namespace TestsStore.Api.Application.Commands.TestCommands
{
	public class CreateOrUpdateTestCommand : ICommand
	{
		public CreateOrUpdateTestCommand(string name, string className, Guid projectId)
		{
			Name = name;
			ClassName = className;
			ProjectId = projectId;
		}

		public string Name { get; }

		public string ClassName { get; }

		public Guid ProjectId { get; }
	}
}