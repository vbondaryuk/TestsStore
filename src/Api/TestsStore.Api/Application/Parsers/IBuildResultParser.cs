using System.IO;

namespace TestsStore.Api.Application.Parsers
{
	public interface IBuildResultParser
	{
		BuildParseResult Parse(Stream stream);
	}
}