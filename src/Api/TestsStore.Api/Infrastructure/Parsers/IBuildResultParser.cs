using System.IO;

namespace TestsStore.Api.Infrastructure.Parsers
{
	public interface IBuildResultParser
	{
		BuildParseResult Parse(Stream stream);
	}
}