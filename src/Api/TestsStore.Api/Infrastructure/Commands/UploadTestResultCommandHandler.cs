using System.Collections.Generic;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Parsers;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class UploadTestResultCommandHandler : ICommandHandler<UploadTestResultCommand>
	{
		private readonly BuildCommandHandler _buildCommandHandler;
		private readonly ProjectCommandHandler _projectCommandHandler;
		private readonly TestResultCommandHandler _testResultCommandHandler;

		public UploadTestResultCommandHandler(
			ProjectCommandHandler projectCommandHandler,
			BuildCommandHandler buildCommandHandler,
			TestResultCommandHandler testResultCommandHandler)
		{
			_projectCommandHandler = projectCommandHandler;
			_buildCommandHandler = buildCommandHandler;
			_testResultCommandHandler = testResultCommandHandler;
		}

		public async Task<ICommandResult> ExecuteAsync(UploadTestResultCommand command)
		{
			var parser = ParserFactory.Create(command.FileType);
			var parseResult = parser.Parse(command.Stream);

			var projectCommandResult = await CreateProject(command);
			if (!projectCommandResult.Success)
				return FailedResult();

			var project = projectCommandResult.Result;

			var buildCommandResult = await CreateBuild(parseResult, project);
			if (!buildCommandResult.Success)
				return FailedResult();

			var build = buildCommandResult.Result;
			var testResultCommandResult = await CreateTestResults(build, parseResult);

			if (!testResultCommandResult.Success)
				return FailedResult();

			return SuccessResult();
		}

		private async Task<ICommandResult<Project>> CreateProject(UploadTestResultCommand command)
		{
			var createProjectCommand = new CreateProjectCommand(command.ProjectName);
			var projectCommandResult = await _projectCommandHandler.ExecuteAsync(createProjectCommand);
			return projectCommandResult;
		}

		private async Task<ICommandResult<Build>> CreateBuild(BuildParseResult parseResult, Project project)
		{
			var createBuildCommand = new CreateBuildCommand(project.Id, parseResult.Name, parseResult.Status,
				parseResult.StartTime, parseResult.EndTime);
			var buildCommandResult = await _buildCommandHandler.ExecuteAsync(createBuildCommand);

			return buildCommandResult;
		}

		private async Task<ICommandResult> CreateTestResults(Build build, BuildParseResult parseResult)
		{
			var testResultCommands = new List<CreateTestResultCommand>();
			foreach (var testResult in parseResult.TestResults)
			{
				var testResultCommand = new CreateTestResultCommand(
					build.ProjectId, build.Id, testResult.Name, testResult.ClassName,
					testResult.Duration, testResult.Status, testResult.Messages, testResult.StackTrace,
					testResult.ErrorMessage);
				testResultCommands.Add(testResultCommand);
			}

			var addBatchTestResultCommand = new AddBatchTestResultCommand(testResultCommands);
			var testResultCommandResult = await _testResultCommandHandler.ExecuteAsync(addBatchTestResultCommand);
			return testResultCommandResult;
		}

		private static CommandResult FailedResult()
		{
			return new CommandResult(false);
		}

		private static CommandResult SuccessResult()
		{
			return new CommandResult(true);
		}
	}
}