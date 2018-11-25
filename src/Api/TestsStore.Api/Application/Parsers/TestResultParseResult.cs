using System;

namespace TestsStore.Api.Application.Parsers
{
	public class TestResultParseResult
	{
		public string Name { get; set; }

		public string ClassName { get; set; }

		public TimeSpan Duration { get; set; }

		public string Status { get; set; }

		public string Messages { get; set; }

		public string StackTrace { get; set; }

		public string ErrorMessage { get; set; }
	}
}