using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TestsStore.Api.Infrastructure.Parsers.Trx.Models;

namespace TestsStore.Api.Infrastructure.Parsers.Trx
{
	public class TrxBuildResultParser : IBuildResultParser
	{
		public BuildParseResult Parse(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(TestRun));
			try
			{
				TestRun testRunResult;
				using (StreamReader streamReader = new StreamReader(stream))
				{
					testRunResult = (TestRun)serializer.Deserialize(streamReader);
				}
				return ParseTestRunResult(testRunResult);
			}
			catch (Exception e)
			{
				throw new ArgumentException("Trx file could not be parsed", e);
			}
		}

		private static BuildParseResult ParseTestRunResult(TestRun testRunResult)
		{
			var status = ResolveStatus(testRunResult.ResultSummary.Outcome);
			var startTime = DateTime.Parse(testRunResult.Times.Start);
			var endTime = DateTime.Parse(testRunResult.Times.Finish);
			List<TestResultParseResult> testResults = RetrieveTestResults(testRunResult);

			var buildParseResult = new BuildParseResult
			{
				Name = testRunResult.Name,
				Status = status,
				StartTime = startTime,
				EndTime = endTime,
				TestResults = testResults
			};

			return buildParseResult;
		}

		private static List<TestResultParseResult> RetrieveTestResults(TestRun testRunResult)
		{
			List<TestResultParseResult> testResults =
				new List<TestResultParseResult>(testRunResult.Results.UnitTestResult.Count);
			Dictionary<string, UnitTest> testDefinitions = testRunResult.TestDefinitions.UnitTest.ToDictionary(x => x.Id);

			foreach (UnitTestResult unitTestResult in testRunResult.Results.UnitTestResult)
			{
				TestResultParseResult createTestResultCommand = RetrieveTestResult(unitTestResult, testDefinitions);
				testResults.Add(createTestResultCommand);
			}

			return testResults;
		}

		private static TestResultParseResult RetrieveTestResult(
			UnitTestResult unitTestResult,
			Dictionary<string, UnitTest> testDefinitions)
		{
			var testDefinition = testDefinitions[unitTestResult.TestId];
			var duration = string.IsNullOrEmpty(unitTestResult.Duration)
				? TimeSpan.Zero
				: TimeSpan.Parse(unitTestResult.Duration);

			var addTestResultCommand = new TestResultParseResult
			{
				Name = testDefinition.TestMethod.Name,
				ClassName = testDefinition.TestMethod.ClassName,
				Duration = duration,
				Status = ResolveStatus(unitTestResult.Outcome, duration)
			};

			if (unitTestResult.Output != null)
			{
				addTestResultCommand.Messages = unitTestResult.Output.StdOut;
				if (unitTestResult.Output.ErrorInfo != null)
				{
					addTestResultCommand.ErrorMessage = unitTestResult.Output.ErrorInfo.Message;
					addTestResultCommand.StackTrace = unitTestResult.Output.ErrorInfo.StackTrace;
				}
			}

			return addTestResultCommand;
		}

		private static string ResolveStatus(string status)
		{
			if (status.Equals("NotExecuted"))
			{
				return "Skipped";
			}

			return status;
		}
		private static string ResolveStatus(string status, TimeSpan duration)
		{
			if (status.Equals("NotExecuted"))
			{
				return duration == TimeSpan.Zero ? "Inconclusive" : "Skipped";
			}

			return status;
		}
	}
}