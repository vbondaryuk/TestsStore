using System.IO;

namespace TestsStore.Api.Infrastructure.Commands
{
	public class UploadTestResultCommand : ICommand
	{
		public string ProjectName { get; set; }
		public string FileType { get; set; }
		public Stream Stream { get; set; }
	}
}