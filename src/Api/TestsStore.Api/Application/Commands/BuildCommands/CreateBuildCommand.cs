using System;

namespace TestsStore.Api.Application.Commands.BuildCommands
{
	public class CreateBuildCommand : ICommand
	{
		public CreateBuildCommand(Guid projectId, string name, string status, DateTime startTime, DateTime endTime)
		{
			Id = Guid.NewGuid(); 
			ProjectId = projectId;
			Name = name;
			Status = status;
			StartTime = startTime;
			EndTime = endTime;
		}

		public Guid Id { get; }

		public Guid ProjectId { get; }

		public string Name { get; }

		public string Status { get; }

		public DateTime StartTime { get; }

		public DateTime EndTime { get; }
	}
}