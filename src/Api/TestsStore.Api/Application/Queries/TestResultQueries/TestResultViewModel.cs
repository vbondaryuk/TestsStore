using System;
using TestsStore.Api.Application.Queries.BuildQueries;
using TestsStore.Api.Application.Queries.TestQueries;

namespace TestsStore.Api.Application.Queries.TestResultQueries
{
	public class TestResultViewModel
	{
		public Guid Id { get; set; }
		
		public TestViewModel Test { get; set; }
		
		public BuildViewModel Build { get; set; }
		
		public string Status { get; set; }

		public int Duration { get; set; }

		public string Messages { get; set; }

		public string StackTrace { get; set; }

		public string ErrorMessage { get; set; }
	}
}