using System;

namespace TestsStore.VSTestLogger.Models
{
	public class Build
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
	}
}