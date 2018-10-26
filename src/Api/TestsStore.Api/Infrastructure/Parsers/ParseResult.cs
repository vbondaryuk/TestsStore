using System.Collections.Generic;
using TestsStore.Api.Infrastructure.Commands;

namespace TestsStore.Api.Infrastructure.Parsers
{
	public class ParseResult
	{
		public AddBuildCommand AddBuildCommand { get; set; }
		public List<AddTestResultCommand> AddTestResultCommands { get; set; }

		public ParseResult(AddBuildCommand addBuildCommand, List<AddTestResultCommand> testResultCommands)
		{
			AddBuildCommand = addBuildCommand;
			AddTestResultCommands = testResultCommands;
		}
	}
}