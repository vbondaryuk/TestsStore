using System;
using System.Collections.Generic;
using TestsStore.Api.Infrastructure.Parsers.Trx;

namespace TestsStore.Api.Infrastructure.Parsers
{
	public class ParserFactory
	{
		private static readonly Dictionary<ParserType, Func<ITestResultParser>> FactoryDictionary =
			new Dictionary<ParserType, Func<ITestResultParser>>
			{
				{ParserType.Trx, () => new TrxTestResultParser()}
			};

		public static ITestResultParser Create(ParserType parserType)
		{
			if (FactoryDictionary.TryGetValue(parserType, out var testResultParser))
			{
				return testResultParser();
			}

			throw new ArgumentException(nameof(parserType));
		}
	}
}