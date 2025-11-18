using Horus.Domain.Tooling.Manifests.Parsers.Types;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parser.Builders
{
	public static class ParserTypeBuilder
	{
		public static ParserType ForRegex() => ParserType.Create("regex");

		public static ParserType ForJsonPath() => ParserType.Create("jsonpath");

		public static ParserType ForXPath() => ParserType.Create("xpath");

		public static ParserType ForLiteral() => ParserType.Create("literal");

		public static ParserType ForCsv() => ParserType.Create("csv");
	}
}