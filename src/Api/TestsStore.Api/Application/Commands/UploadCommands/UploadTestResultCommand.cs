using System.IO;

namespace TestsStore.Api.Application.Commands.UploadCommands
{
	public class UploadTestResultCommand : ICommand
	{
		public UploadTestResultCommand(string projectName, string fileType, Stream stream)
		{
			ProjectName = projectName;
			FileType = fileType;
			Stream = stream;
		}

		public string ProjectName { get; }
		public string FileType { get; }
		public Stream Stream { get; }
	}
}