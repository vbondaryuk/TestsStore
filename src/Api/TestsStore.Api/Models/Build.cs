using System;
using System.Collections.Generic;

namespace TestsStore.Api.Models
{
	public class Build
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public Guid StatusId { get; set; }

		public Status Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public Guid ProjectId { get; set; }

		public Project Project { get; set; }

		public int Duration => EndTime == DateTime.MinValue ? 0 : (int)EndTime.Subtract(StartTime).TotalMilliseconds;

		public ICollection<TestResult> TestResults { get; set; }
	}
}