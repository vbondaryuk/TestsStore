using System;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace TestsStore.VSTestLogger.Models
{
	public class TestMethodResult
	{
		public string Name { get; }

		public string ClassName { get; }

		public TimeSpan Duration { get; }

		public string Outcome { get; }

		public string Messages { get; }

		public string StackTrace { get; }

		public string ErrorMessage { get; }

		public TestMethodResult(TestResult testResult)
		{
			Name = testResult.TestCase.DisplayName;
			ClassName = testResult.TestCase.FullyQualifiedName.Substring(0, testResult.TestCase.FullyQualifiedName.LastIndexOf('.'));
			Duration = testResult.Duration;
			Outcome = RetrieveOutcome(testResult);
			Messages = string.Join(Environment.NewLine, testResult.Messages.Select(x => x.Text));
			ErrorMessage = testResult.ErrorMessage;
			StackTrace = testResult.ErrorStackTrace;
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