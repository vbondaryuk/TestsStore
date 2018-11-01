using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class UpdateBuildCommand : ICommand
	{
		public UpdateBuildCommand(Guid id, string status, DateTime endTime)
		{
			Id = id;
			Status = status;
			EndTime = endTime;
		}

		public Guid Id { get;}

		public string Status { get; }

		public DateTime EndTime { get; }
	}
}