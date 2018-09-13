using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using TestsStore.VSTestLogger.Models;
using TestsStore.VSTestLogger.Services;

namespace TestsStore.VSTestLogger
{
	[FriendlyName(Constants.LoggerFriendlyName)]
	[ExtensionUri(Constants.LoggerExtensionUri)]
	public class Logger : ITestLoggerWithParameters
	{
		private Dictionary<string, string> parametersDictionary;
		private DateTime startTime;
		private Project project;
		private Build build;


		private ITestsStoreService testsStoreService;

		public void Initialize(TestLoggerEvents events, string testRunDirectory)
		{
			startTime = DateTime.Now;

			if (this.parametersDictionary.TryGetValue(Constants.TestsStoreApiUrl, out string testsStoreApiUrl)
				&& this.parametersDictionary.TryGetValue(Constants.Project, out string projectName)
				&& this.parametersDictionary.TryGetValue(Constants.Build, out string buildName))
			{
				testsStoreService = new TestsStoreService(testsStoreApiUrl);
				project = testsStoreService.GetProjectAsync(projectName).GetAwaiter().GetResult();
				build = new Build
				{
					Id = Guid.NewGuid(),
					Name = buildName,
					StartTime = DateTime.Now
				};
				testsStoreService.AddBuildAsync(project, build).GetAwaiter().GetResult();
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
			testsStoreService.UpdateBuildAsync(build).GetAwaiter().GetResult();
		}

		private void OnTestResult(object sender, TestResultEventArgs e)
		{
			TestResult testresult = e.Result;
			var testMethodResult = new TestMethodResult(testresult);
			testsStoreService.AddTestAsync(project, build, testMethodResult).GetAwaiter().GetResult();
		}
	}
}