using System.IO;
using System.Threading.Tasks;
using TestsStore.Api.Infrastructure.Parsers;

namespace TestsStore.Api.Infrastructure.Services
{
	public class UploadTestResultService
	{
		public UploadTestResultService()
		{
		}

		public async Task UplodAsync(string projectName, Stream testResultStream, ParserType parserType)
		{
			var parser = ParserFactory.Create(parserType);
			var parseResult = parser.Parse(testResultStream);

		}
	}
}