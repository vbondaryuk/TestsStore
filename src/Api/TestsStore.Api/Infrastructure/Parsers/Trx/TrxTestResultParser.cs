using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TestsStore.Api.Infrastructure.Commands;
using TestsStore.Api.Infrastructure.Parsers.Trx.Models;

namespace TestsStore.Api.Infrastructure.Parsers.Trx
{
	public class TrxTestResultParser : ITestResultParser
	{
		public ParseResult Parse(Stream stream)
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

		private ParseResult ParseTestRunResult(TestRun testRunResult)
		{
			var addBuildCommand = new CreateBuildCommand
			{
				Name = testRunResult.Name,
				StartTime = DateTime.Parse(testRunResult.Times.Start),
				EndTime = DateTime.Parse(testRunResult.Times.Finish),
				Status = ResolveStatus(testRunResult.ResultSummary.Outcome)
			};

			List<CreateTestResultCommand> testResultCommands = CreateAddTestResultCommands(testRunResult);

			return new ParseResult(addBuildCommand,testResultCommands);
		}

		private static List<CreateTestResultCommand> CreateAddTestResultCommands(TestRun testRunResult)
		{
			List<CreateTestResultCommand> testResultCommands =
				new List<CreateTestResultCommand>(testRunResult.Results.UnitTestResult.Count);
			Dictionary<string, UnitTest> testDefinitions = testRunResult.TestDefinitions.UnitTest.ToDictionary(x => x.Id);

			foreach (UnitTestResult unitTestResult in testRunResult.Results.UnitTestResult)
			{
				CreateTestResultCommand createTestResultCommand = CreateAddTestResultCommand(unitTestResult, testDefinitions);
				
				testResultCommands.Add(createTestResultCommand);
			}

			return testResultCommands;
		}

		private static CreateTestResultCommand CreateAddTestResultCommand(
			UnitTestResult unitTestResult,
			Dictionary<string, UnitTest> testDefinitions)
		{
			var testDefinition = testDefinitions[unitTestResult.TestId];
			var addTestResultCommand = new CreateTestResultCommand
			{
				Name = testDefinition.TestMethod.Name,
				ClassName = testDefinition.TestMethod.ClassName,
				Duration = string.IsNullOrEmpty(unitTestResult.Duration)
					? TimeSpan.Zero
					: TimeSpan.Parse(unitTestResult.Duration),
				Status = ResolveStatus(unitTestResult.Outcome)
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
	}
}