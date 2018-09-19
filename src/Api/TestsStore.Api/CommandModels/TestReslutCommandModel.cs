using System;

namespace TestsStore.Api.CommandModels
{
	public class TestReslutCommandModel
	{
		public Guid BuildId { get; set; }

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string ClassName { get; set; }

		public TimeSpan Duration { get; set; }

		public string Status { get; set; }

		public string Messages { get; set; }

		public string StackTrace { get; set; }

		public string ErrorMessage { get; set; }
	}
}