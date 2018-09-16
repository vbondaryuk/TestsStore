using System;
using System.Collections.Generic;

namespace TestsStore.Api.Models
{
	public class Project
	{
		public Project()
		{ }

		public Project(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public ICollection<Build> Builds { get; set; }

		public ICollection<Test> Tests { get; set; }
	}
}