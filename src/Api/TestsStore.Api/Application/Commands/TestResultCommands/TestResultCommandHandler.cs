using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Application.Commands.TestCommands;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.TestResultCommands
{
	public class TestResultCommandHandler : ITestResultCommandHandler
	{
		private readonly ITestCommandHandler _testCommandHandler;
		private readonly ITestResultRepository _testResultRepository;

		public TestResultCommandHandler(
			ITestCommandHandler testCommandHandler,
			ITestResultRepository testResultRepository)
		{
			_testCommandHandler = testCommandHandler;
			_testResultRepository = testResultRepository;
		}

		public async Task<ICommandResult> ExecuteAsync(AddBatchTestResultCommand command)
		{
			var testResults = new List<TestResult>(command.CreateTestResultCommands.Count);

			foreach (var createTestResultCommand in command.CreateTestResultCommands)
			{
				var testResult = await CreateTestResultAsync(createTestResultCommand);
				testResults.Add(testResult);
			}

			await _testResultRepository.AddRange(testResults);

			return new CommandResult(true);
		}

		public async Task<ICommandResult<TestResult>> ExecuteAsync(CreateTestResultCommand command)
		{
			var testResult = await CreateTestResultAsync(command);

			await _testResultRepository.Add(testResult);

			return new CommandResult<TestResult>(true, testResult);
		}

		private async Task<TestResult> CreateTestResultAsync(CreateTestResultCommand command)
		{
			var status = Enumeration.FromDisplayName<Status>(command.Status);
			var test = await GetTestAsync(command);

			var testResult = new TestResult
			{
				Id = Guid.NewGuid(),
				TestId = test.Id,
				BuildId = command.BuildId,
				Duration = command.Duration.Milliseconds,
				StatusId = status.Id,
				Messages = command.Messages,
				StackTrace = command.StackTrace,
				ErrorMessage = command.ErrorMessage
			};
			return testResult;
		}

		private async Task<Test> GetTestAsync(CreateTestResultCommand command)
		{
			var createOrUpdateTestCommand =
				new CreateOrUpdateTestCommand(command.Name, command.ClassName, command.ProjectId);

			var createOrUpdateTestCommandResult = await _testCommandHandler.ExecuteAsync(createOrUpdateTestCommand);
			var test = createOrUpdateTestCommandResult.Result;

			return test;
		}
	}
}