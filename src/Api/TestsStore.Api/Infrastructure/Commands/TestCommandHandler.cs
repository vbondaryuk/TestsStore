using System;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class TestCommandHandler : ICommandHandler<CreateOrUpdateTestCommand, Test>
	{
		private readonly ITestRepository _testRepository;

		public TestCommandHandler(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		public async Task<ICommandResult<Test>> ExecuteAsync(CreateOrUpdateTestCommand command)
		{
			var test = await _testRepository.Get(command.Name, command.ClassName, command.ProjectId);

			if (test == null)
			{
				test = new Test
				{
					Id = Guid.NewGuid(),
					Name = command.Name,
					ClassName = command.ClassName,
					ProjectId = command.ProjectId
				};
				await _testRepository.Add(test);
			}

			return new CommandResult<Test>(true, test);
		}
	}
}