using System;

namespace TestsStore.Api.Application.Commands.TestResultCommands
{
	public class CreateTestResultCommand : ICommand
	{
		public CreateTestResultCommand(
			Guid projectId, 
			Guid buildId, 
			string name, 
			string className, 
			TimeSpan duration,
			string status, 
			string messages, 
			string stackTrace, 
			string errorMessage)
		{
			ProjectId = projectId;
			BuildId = buildId;
			Name = name;
			ClassName = className;
			Duration = duration;
			Status = status;
			Messages = messages;
			StackTrace = stackTrace;
			ErrorMessage = errorMessage;
		}

		public Guid ProjectId { get; }

		public Guid BuildId { get; }

		public string Name { get; }

		public string ClassName { get; }

		public TimeSpan Duration { get; }

		public string Status { get; }

		public string Messages { get; }

		public string StackTrace { get; }

		public string ErrorMessage { get; }
	}
}