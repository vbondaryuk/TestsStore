using System;

namespace TestsStore.Api.Models
{
	public class Status : Enumeration
	{
		public static readonly Status None = new Status(Guid.Parse("a07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "None");
		public static readonly Status Passed = new Status(Guid.Parse("b07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "Passed");
		public static readonly Status Failed = new Status(Guid.Parse("c07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "Failed");
		public static readonly Status Skipped = new Status(Guid.Parse("d07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "Skipped");
		public static readonly Status Inconclusive = new Status(Guid.Parse("e07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "Inconclusive");
		public static readonly Status NotFound = new Status(Guid.Parse("f07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"), "NotFound");

		private Status() {}

		private Status(Guid id, string name)
			:base(id, name)
		{
		}
	}
}