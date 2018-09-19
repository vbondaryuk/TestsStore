using System;

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
	}
}