using System;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class TestResultCommandHandler : ICommandHandler<CreateTestResultCommand, TestResult>
	{
		private readonly TestCommandHandler _testCommandHandler;
		private readonly ITestResultRepository _testResultRepository;

		public TestResultCommandHandler(
			TestCommandHandler testCommandHandler,
			ITestResultRepository testResultRepository)
		{
			_testCommandHandler = testCommandHandler;
			_testResultRepository = testResultRepository;
		}

		public async Task<ICommandResult<TestResult>> ExecuteAsync(CreateTestResultCommand command)
		{
			var status = Enumeration.FromDisplayName<Status>(command.Status);
			var test = await GetTest(command);

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

			await _testResultRepository.Add(testResult);

			return new CommandResult<TestResult>(true, testResult);
		}

		private async Task<Test> GetTest(CreateTestResultCommand command)
		{
			var createOrUpdateTestCommand =
				new CreateOrUpdateTestCommand
				{
					Name = command.Name,
					ClassName = command.ClassName,
					ProjectId = command.ProjectId
				};

			var createOrUpdateTestCommandResult = await _testCommandHandler.ExecuteAsync(createOrUpdateTestCommand);
			var test = createOrUpdateTestCommandResult.Result;

			return test;
		}
	}
}