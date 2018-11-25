using System;
using System.Collections.Generic;

namespace TestsStore.Api.Application.Parsers
{
	public class BuildParseResult
	{
		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public ICollection<TestResultParseResult> TestResults { get; set; }
	}
}