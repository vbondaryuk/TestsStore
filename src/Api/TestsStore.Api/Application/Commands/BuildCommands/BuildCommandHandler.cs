using System;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Repositories;
using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Commands.BuildCommands
{
	public class BuildCommandHandler : IBuildCommandHandler
	{
		private readonly IBuildRepository _buildRepository;

		public BuildCommandHandler(IBuildRepository buildRepository)
		{
			_buildRepository = buildRepository;
		}

		public async Task<ICommandResult<Build>> ExecuteAsync(CreateBuildCommand command)
		{
			Status status = Enumeration.FromDisplayName<Status>(command.Status);
			Build build = new Build
			{
				Id = Guid.NewGuid(),
				ProjectId = command.ProjectId,
				Name = command.Name,
				StatusId = status.Id,
				StartTime = command.StartTime,
				EndTime = command.EndTime
			};

			await _buildRepository.AddAsync(build);

			return new CommandResult<Build>(true, build);
		}

		public async Task<ICommandResult<Build>> ExecuteAsync(UpdateBuildCommand command)
		{
			var build = await _buildRepository.GetByIdAsync(command.Id);
			if (build == null)
			{
				return new CommandResult<Build>(false, null);
			}

			var status = Enumeration.FromDisplayName<Status>(command.Status);
			build.EndTime = command.EndTime;
			build.StatusId = status.Id;

			await _buildRepository.UpdatedAsync(build);

			return new CommandResult<Build>(true, build);
		}
	}
}