using System.Collections.Generic;
using System.Linq;
using TestsStore.Api.Application.Queries.BuildQueries;
using TestsStore.Api.Application.Queries.ProjectQueries;
using TestsStore.Api.Application.Queries.TestQueries;
using TestsStore.Api.Application.Queries.TestResultQueries;
using TestsStore.Api.Models;

namespace TestsStore.Api.Application.Queries
{
	public static class Mapper
	{
		public static ICollection<BuildViewModel> Map(IEnumerable<Build> builds)
		{
			return builds.Select(Map).ToList();
		}

		public static BuildViewModel Map(Build build)
		{
			if (build is null)
			{
				return null;
			}

			return new BuildViewModel
			{
				Id = build.Id,
				ProjectId = build.ProjectId,
				Name = build.Name,
				Status = build.Status.Name,
				StartTime = build.StartTime,
				EndTime = build.EndTime,
				Duration = build.Duration
			};
		}

		public static ICollection<ProjectViewModel> Map(IEnumerable<Project> projects)
		{
			return projects.Select(Map).ToList();
		}

		public static ProjectViewModel Map(Project project)
		{
			return new ProjectViewModel
			{
				Id = project.Id,
				Name = project.Name
			};
		}

		public static ICollection<TestViewModel> Map(IEnumerable<Test> tests)
		{
			return tests.Select(Map).ToList();
		}

		public static TestViewModel Map(Test test)
		{
			return new TestViewModel
			{
				Id = test.Id,
				Name = test.Name,
				ClassName = test.ClassName
			};
		}

		public static ICollection<TestResultViewModel> Map(IEnumerable<TestResult> testResults)
		{
			return testResults.Select(Map).ToList();
		}

		public static TestResultViewModel Map(TestResult testResult)
		{
			return new TestResultViewModel
			{
				Id = testResult.Id,
				Test = Map(testResult.Test),
				Build = Map(testResult.Build),
				Status = testResult.Status.Name,
				Duration = testResult.Duration,
				Messages = testResult.Messages,
				ErrorMessage = testResult.ErrorMessage,
				StackTrace = testResult.StackTrace
			};
		}
	}
}