using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace TestsStore.VSTestLogger
{
	[FriendlyName(FriendlyName)]
	[ExtensionUri(ExtensionUri)]
	public class Logger : ITestLoggerWithParameters
	{
		private const string ExtensionUri = "logger://Microsoft/TestPlatform/TestStoreLogger/v1";
		private const string FriendlyName = "TestStoreLogger";

		private Dictionary<string, string> parametersDictionary;

		public void Initialize(TestLoggerEvents events, string testRunDirectory)
		{
			events.TestRunStart += OnTestRunStart;
			events.TestResult += OnTestResult;
			events.TestRunComplete += OnTestRunComplete;
		}

		public void Initialize(TestLoggerEvents events, Dictionary<string, string> parameters)
		{
			parametersDictionary = parameters;
			Initialize(events, this.parametersDictionary[DefaultLoggerParameterNames.TestRunDirectory]);
		}

		private void OnTestRunStart(object sender, TestRunStartEventArgs e)
		{

		}

		private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
		{

		}

		private void OnTestResult(object sender, TestResultEventArgs e)
		{
			
		}
	}
}