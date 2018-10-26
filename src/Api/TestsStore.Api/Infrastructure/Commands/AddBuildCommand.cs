using System;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class AddBuildCommand
	{
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public AddBuildCommand()
		{}

		public AddBuildCommand(Guid id, Guid projectId, string name, string status, DateTime startTime, DateTime endTime)
		{
			Id = id;
			ProjectId = projectId;
			Name = name;
			Status = status;
			StartTime = startTime;
			EndTime = endTime;
		}

		public Build ToBuild()
		{
			Status status = Enumeration.FromDisplayName<Status>(Status);
			Build build = new Build
			{
				Id = Guid.NewGuid(),
				ProjectId = ProjectId,
				Name = Name,
				StatusId = status.Id,
				StartTime = StartTime,
				EndTime = EndTime
			};

			return build;
		}
	}
}