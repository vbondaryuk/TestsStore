using System;

namespace TestsStore.Api.QueryModels
{
	public class BuildQueryModel
	{
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
	}
}