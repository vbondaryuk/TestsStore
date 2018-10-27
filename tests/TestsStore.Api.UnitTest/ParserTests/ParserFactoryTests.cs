﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsStore.Api.Infrastructure.Parsers;
using TestsStore.Api.Infrastructure.Parsers.Trx;

namespace TestsStore.Api.UnitTest.ParserTests
{
	[TestClass]
	public class ParserFactoryTests
	{
		[TestMethod]
		public void Create_TrxParser_Should_Be_Created()
		{
			var parser = ParserFactory.Create(ParserType.Trx);

			Assert.IsTrue(parser is TrxTestResultParser);
		}

		[TestMethod]
		public void Create_Parser_Incorrect_Type_Should_ArgumentException()
		{
			Assert.ThrowsException<ArgumentException>(() => ParserFactory.Create((ParserType) 999));
		}

	}
}