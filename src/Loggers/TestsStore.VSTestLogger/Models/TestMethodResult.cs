using System;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace TestsStore.VS.TestLogger.Models
{
	public class TestMethodResult
	{
		public Guid ProjectId { get; }

		public Guid BuildId { get; }

		public string Name { get; }

		public string ClassName { get; }

		public TimeSpan Duration { get; }

		public string Status { get; }

		public string Messages { get; }

		public string StackTrace { get; }

		public string ErrorMessage { get; }

		public TestMethodResult(Guid projectId, Guid buildId, TestResult testResult)
		{
			ProjectId = projectId;
			BuildId = buildId;
			Name = testResult.TestCase.DisplayName;
			ClassName = testResult.TestCase.FullyQualifiedName.Substring(0, testResult.TestCase.FullyQualifiedName.LastIndexOf('.'));
			Duration = testResult.Duration;
			Status = RetrieveOutcome(testResult);
			Messages = WebUtility.HtmlEncode(string.Join(Environment.NewLine, testResult.Messages.Select(x => x.Text)));
			ErrorMessage = WebUtility.HtmlEncode(testResult.ErrorMessage);
			StackTrace = WebUtility.HtmlEncode(testResult.ErrorStackTrace);
		}

		private string RetrieveOutcome(TestResult testResult)
		{
			if (testResult.Outcome == TestOutcome.Skipped && Duration != TimeSpan.Zero)
			{
				return "Inconclusive";
			}

			return Enum.GetName(typeof(TestOutcome), testResult.Outcome);
		}
	}



}