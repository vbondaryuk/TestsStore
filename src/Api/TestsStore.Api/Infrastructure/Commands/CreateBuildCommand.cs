using System;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class CreateBuildCommand : ICommand
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid ProjectId { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
	}
}