using System;
using System.Collections.Generic;

namespace TestsStore.Api.Model
{
	public class Test
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string FullName { get; set; }

		public Guid ProjectId { get; set; }

		public Project Project { get; set; }

		public ICollection<TestResult> TestResults { get; set; }
	}
}