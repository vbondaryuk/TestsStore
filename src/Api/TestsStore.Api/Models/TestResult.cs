using System;

namespace TestsStore.Api.Models
{
	public class TestResult
	{
		public Guid Id { get; set; }

		public Guid TestId { get; set; }

		public Test Test { get; set; }

		public Guid BuildId { get; set; }

		public Build Build { get; set; }

		public TimeSpan Duration { get; set; }

		public Guid StatusId { get; set; }

		public Status Status { get; set; }

		public string Messages { get; set; }

		public string StackTrace { get; set; }

		public string ErrorMessage { get; set; }
	}
}