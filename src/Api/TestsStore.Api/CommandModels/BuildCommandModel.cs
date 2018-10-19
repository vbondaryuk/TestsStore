using System;
using TestsStore.Api.Models;

namespace TestsStore.Api.CommandModels
{
	public class BuildCommandModel
	{
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

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