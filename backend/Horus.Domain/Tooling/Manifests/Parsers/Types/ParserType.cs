using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types
{
	public sealed class ParserType : ValueObject
	{
		public string Value { get; }

		// Supported parser types
		private static readonly HashSet<string> SupportedTypes = [
			"regex",
			"jsonpath",
			"xpath",
			"literal",
			"csv"
		];

		private ParserType(string value)
		{
			Value = value;
		}

		public static ParserType Create(string rawType)
		{
			CheckRule(new StringCannotBeEmptyOrNull(rawType, "ToolResultParser.Type"));

			var normalized = rawType.Trim().ToLowerInvariant();

			CheckRule(new Rules.ParserTypeCannotAcceptNotSupportedType(
				SupportedTypes,
				normalized
			));

			return new ParserType(normalized);
		}

		/// <summary>
		/// Validates if the provided parser data is compatible with the declared parser type.
		/// </summary>
		public void ValidateParserData(object? parserData)
		{
			if (parserData is null)
				return;

			switch (Value)
			{
				case "regex":
					CheckRule(new Rules.Regex.ParserTypeMustReceiveValidRegexList(parserData));
					break;

				case "jsonpath":
					CheckRule(new Rules.Json.ParserTypeMustReceiveValidJsonPath(parserData));
					break;

				case "xpath":
					CheckRule(new Rules.Xml.ParserTypeMustReceiveValidXPath(parserData));
					break;

				case "literal":
					CheckRule(new Rules.Literal.ParserTypeLiteralMustReceiveValidLiteral(parserData));
					break;

				case "csv":
					CheckRule(new Rules.Csv.ParserTypeCsvMustReceiveValidCsvPattern(parserData));
					break;
			}
		}
	}
}