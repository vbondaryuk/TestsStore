using System;
using System.Collections.Generic;

namespace TestsStore.Api.Models
{
	public class Test
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string ClassName { get; set; }

		public Guid ProjectId { get; set; }

		public Project Project { get; set; }

		public ICollection<TestResult> TestResults { get; set; }
	}
}