using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Parsers;

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

			var createProjectCommand = new CreateProjectCommand
			{
				Name = command.ProjectName
			};
			var projectCommandResult = await _projectCommandHandler.ExecuteAsync(createProjectCommand);
			if (!projectCommandResult.Success)
				return FailedResult();

			var createBuildCommand = parseResult.CreateBuildCommand;
			createBuildCommand.ProjectId = projectCommandResult.Result.Id;
			var buildCommandResult = await _buildCommandHandler.ExecuteAsync(createBuildCommand);
			if (!buildCommandResult.Success)
				return FailedResult();

			foreach (var addTestResultCommand in parseResult.CreateTestResultCommands)
			{
				addTestResultCommand.ProjectId = projectCommandResult.Result.Id;
				addTestResultCommand.BuildId = buildCommandResult.Result.Id;
			}

			var addBatchTestResultCommand = new AddBatchTestResultCommand
				{CreateTestResultCommands = parseResult.CreateTestResultCommands};
			var testResultCommandResult = await _testResultCommandHandler.ExecuteAsync(addBatchTestResultCommand);

			if (!testResultCommandResult.Success)
				return FailedResult();

			return SuccessResult();
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