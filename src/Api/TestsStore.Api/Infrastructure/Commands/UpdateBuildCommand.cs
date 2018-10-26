﻿using System;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class UpdateBuildCommand
	{
		public Guid Id { get; set; }

		public string Status { get; set; }

		public DateTime EndTime { get; set; }
	}
}