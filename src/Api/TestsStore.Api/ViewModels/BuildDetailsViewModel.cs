using System;
using System.Collections.Generic;
using TestsStore.Api.Models;

namespace TestsStore.Api.ViewModels
{
	public class BuildDetailsViewModel
	{
		public Guid Id { get; }

		public string Name { get; }

		public string Status { get; }

		public DateTime StartTime { get; }

		public DateTime EndTime { get; }

		public List<TestsSummaryViewModel> TestsSummary { get; }

		public int Duration => EndTime == DateTime.MinValue ? 0 : EndTime.Subtract(StartTime).Seconds;

		public BuildDetailsViewModel(Build build, List<TestsSummaryViewModel> testsSummary)
		{
			Id = build.Id;
			Name = build.Name;
			Status = build.Status.Name;
			StartTime = build.StartTime;
			EndTime = build.EndTime;
			TestsSummary = testsSummary;
		}
	}
}