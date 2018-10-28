using System.Collections.Generic;
using TestsStore.Api.Infrastructure.Commands;

namespace TestsStore.Api.Infrastructure.Parsers
{
	public class ParseResult
	{
		public CreateBuildCommand CreateBuildCommand { get; set; }
		public List<CreateTestResultCommand> CreateTestResultCommands { get; set; }

		public ParseResult(CreateBuildCommand createBuildCommand, List<CreateTestResultCommand> testResultCommands)
		{
			CreateBuildCommand = createBuildCommand;
			CreateTestResultCommands = testResultCommands;
		}
	}
}