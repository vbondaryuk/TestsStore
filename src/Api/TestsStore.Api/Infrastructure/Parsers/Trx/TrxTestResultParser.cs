using System;
using System.Collections.Generic;
using System.IO;
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
			var addBuildCommand = new AddBuildCommand
			{
				Name = testRunResult.Name,
				StartTime = DateTime.Parse(testRunResult.Times.Start),
				EndTime = DateTime.Parse(testRunResult.Times.Finish),
				Status = ResolveStatus(testRunResult.ResultSummary.Outcome)
			};

			List<AddTestResultCommand> testResultCommands = new List<AddTestResultCommand>(testRunResult.Results.UnitTestResult.Count);
			foreach (UnitTestResult unitTestResult in testRunResult.Results.UnitTestResult)
			{
				AddTestResultCommand addTestResultCommand = CreateAddTestResultCommand(unitTestResult);

				testResultCommands.Add(addTestResultCommand);
			}

			return new ParseResult(addBuildCommand,testResultCommands);
		}

		private static AddTestResultCommand CreateAddTestResultCommand(UnitTestResult unitTestResult)
		{
			var addTestResultCommand = new AddTestResultCommand
			{
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