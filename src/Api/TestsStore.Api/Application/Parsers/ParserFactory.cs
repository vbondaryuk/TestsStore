using System;
using System.Collections.Generic;
using TestsStore.Api.Application.Parsers.Trx;

namespace TestsStore.Api.Application.Parsers
{
	public static class ParserFactory
	{
		private static readonly Dictionary<ParserType, Func<IBuildResultParser>> FactoryDictionary =
			new Dictionary<ParserType, Func<IBuildResultParser>>
			{
				{ParserType.Trx, () => new TrxBuildResultParser()}
			};

		public static IBuildResultParser Create(ParserType parserType)
		{
			if (FactoryDictionary.TryGetValue(parserType, out var testResultParser))
			{
				return testResultParser();
			}

			throw new ArgumentException(nameof(parserType));
		}

		public static IBuildResultParser Create(string parserType)
		{
			ParserType type = Enum.Parse<ParserType>(parserType, true);

			return Create(type);
		}
	}
}