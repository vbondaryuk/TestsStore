using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using TestsStore.VS.TestLogger.Models;
using TestsStore.VS.TestLogger.Services;

namespace TestsStore.VS.TestLogger
{
	[FriendlyName(Constants.LoggerFriendlyName)]
	[ExtensionUri(Constants.LoggerExtensionUri)]
	internal class TestStoreLogger : ITestLoggerWithParameters
	{
		private Dictionary<string, string> parametersDictionary;
		private Project project;
		private Build build;
		private bool isFailed;

		private ITestsStoreService testsStoreService;

		public void Initialize(TestLoggerEvents events, string testRunDirectory)
		{
			Console.WriteLine($"Initializing {Constants.LoggerFriendlyName}.");

			var value = string.Join(Environment.NewLine, parametersDictionary.Select(x => $"key:{x.Key} value:{x.Value}"));
			Console.WriteLine(value);
			if (this.parametersDictionary.TryGetValue(Constants.Project, out string projectName)
				&& this.parametersDictionary.TryGetValue(Constants.Build, out string buildName)
				&& this.parametersDictionary.TryGetValue(Constants.Url, out string testsStoreApiUrl))
			{
				testsStoreService = new TestsStoreService(testsStoreApiUrl);
				project = testsStoreService.GetProjectAsync(projectName).GetAwaiter().GetResult();

				build = new Build
				{
					ProjectId = project.Id,
					Name = buildName,
					StartTime = DateTime.Now,
					Status = "None"
				};

				build = testsStoreService.AddBuildAsync(build).GetAwaiter().GetResult();
			}
			else
			{
				return;
			}

			events.TestResult += OnTestResult;
			events.TestRunComplete += OnTestRunComplete;
		}

		public void Initialize(TestLoggerEvents events, Dictionary<string, string> parameters)
		{
			parametersDictionary = parameters;
			Initialize(events, this.parametersDictionary[DefaultLoggerParameterNames.TestRunDirectory]);
		}

		private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
		{
			build.EndTime = DateTime.Now;
			build.Status = isFailed ? "Failed" : "Passed";
			testsStoreService.UpdateBuildAsync(build).GetAwaiter().GetResult();
		}

		private void OnTestResult(object sender, TestResultEventArgs e)
		{
			TestResult testResult = e.Result;
			var testMethodResult = new TestMethodResult(project.Id, build.Id, testResult);
			testsStoreService.AddTestAsync(testMethodResult).GetAwaiter().GetResult();

			if (!isFailed && testResult.Outcome == TestOutcome.Failed)
			{
				isFailed = true;
			}
		}
	}
}