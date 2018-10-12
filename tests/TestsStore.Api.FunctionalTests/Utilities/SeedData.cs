using System;
using TestsStore.Api.Infrastructure;
using TestsStore.Api.Models;

namespace TestsStore.Api.FunctionalTests.Utilities
{
	public class SeedData
	{
		public static readonly Project TestProject = new Project { Id = Guid.NewGuid(), Name = "TestProject" };

		public static void FillDataBase(TestsStoreContext context)
		{
			context.Statuses.AddRange(Enumeration.GetAll<Status>());
			context.Projects.Add(TestProject);
		}
	}
}