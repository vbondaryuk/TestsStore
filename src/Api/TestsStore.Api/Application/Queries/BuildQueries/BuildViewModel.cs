using System;

namespace TestsStore.Api.Application.Queries.BuildQueries
{
	public class BuildViewModel
	{
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public int Duration { get; set; }
	}
}