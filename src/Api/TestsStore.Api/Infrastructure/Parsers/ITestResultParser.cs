using System.IO;

namespace TestsStore.Api.Infrastructure.Parsers
{
	public interface ITestResultParser
	{
		ParseResult Parse(Stream stream);
	}
}