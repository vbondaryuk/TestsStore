using System;

namespace TestsStore.VSTestLogger.Models
{
	public class Build
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDateTime { get; set; }
	}
}