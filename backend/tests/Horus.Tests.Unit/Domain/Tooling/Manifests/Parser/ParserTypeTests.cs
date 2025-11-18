using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parsers.Types;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Parser.Builders;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Regex;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Json;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Xml;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Literal;
using Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Csv;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parser
{
	public class ParserTypeTests
	{
		#region Invalid Construction Cases
		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenEmptyTypeIsReceived()
		{
			string emptyType = string.Empty;
			var rule = new StringCannotBeEmptyOrNull(emptyType, "ToolResultParser.Type");

			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParserType.Create(emptyType));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenNullTypeIsReceived()
		{
			string? emptyType = null;
			var rule = new StringCannotBeEmptyOrNull(emptyType!, "ToolResultParser.Type");

			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParserType.Create(emptyType!));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenNotSupportedTypeIsReceived()
		{
			string notSupportedType = "yaml";
			var rule = new ParserTypeCannotAcceptNotSupportedType(["regex"], notSupportedType);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParserType.Create(notSupportedType));

			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Construction Cases
		[Fact]
		public void Create_ShouldReturnValidParserType_WhenSupportedTypeIsReceived()
		{
			string supportedType = "regex";

			var result = ParserType.Create(supportedType);

			Assert.NotNull(result);
			Assert.Equal(supportedType, result.Value);
		}
		#endregion

		#region ValidateParserData - Regex
		[Fact]
		public void ValidateData_ShouldThrowBusinessRuleValidationException_WhenTypeIsRegexAndEmptyPatternsAreReceived()
		{
			var parserType = ParserTypeBuilder.ForRegex();
			IEnumerable<RegexPattern> patterns = [];
			var rule = new ParserTypeMustReceiveValidRegexList(patterns);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => parserType.ValidateParserData(patterns));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateData_ShouldReturn_WhenTypeIsRegexAndValidPatternsAreReceived()
		{
			var parserType = ParserTypeBuilder.ForRegex();
			IEnumerable<RegexPattern> patterns = [RegexPattern.Create("key", ".*", "critical")];

			var exc = Record.Exception(() => parserType.ValidateParserData(patterns));

			Assert.Null(exc);
		}
		#endregion

		#region ValidateParserData - JsonPath
		[Fact]
		public void ValidateData_ShouldThrowBusinessRuleValidationException_WhenTypeIsJsonPathAndInvalidJsonPathIsReceived()
		{
			var parserType = ParserTypeBuilder.ForJsonPath();
			string invalid = "invalid";
			var rule = new ParserTypeMustReceiveValidJsonPath(invalid);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => parserType.ValidateParserData(invalid));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateData_ShouldReturn_WhenTypeIsJsonPathAndValidJsonPathIsReceived()
		{
			var parserType = ParserTypeBuilder.ForJsonPath();
			string valid = "$.data.value";

			var exc = Record.Exception(() => parserType.ValidateParserData(valid));

			Assert.Null(exc);
		}
		#endregion

		#region ValidateParserData - XPath
		[Fact]
		public void ValidateData_ShouldThrowBusinessRuleValidationException_WhenTypeIsXPathAndInvalidSelectorIsReceived()
		{
			var parserType = ParserTypeBuilder.ForXPath();
			string invalid = "selector";
			var rule = new ParserTypeMustReceiveValidXPath(invalid);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => parserType.ValidateParserData(invalid));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateData_ShouldReturn_WhenTypeIsXPathAndValidSelectorIsReceived()
		{
			var parserType = ParserTypeBuilder.ForXPath();
			string valid = "/root/node";

			var exc = Record.Exception(() => parserType.ValidateParserData(valid));

			Assert.Null(exc);
		}
		#endregion

		#region ValidateParserData - Literal
		[Fact]
		public void ValidateData_ShouldThrowBusinessRuleValidationException_WhenTypeIsLiteralAndEmptyLiteralIsReceived()
		{
			var parserType = ParserTypeBuilder.ForLiteral();
			string invalid = "";
			var rule = new ParserTypeLiteralMustReceiveValidLiteral(invalid);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => parserType.ValidateParserData(invalid));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateData_ShouldReturn_WhenTypeIsLiteralAndValidLiteralIsReceived()
		{
			var parserType = ParserTypeBuilder.ForLiteral();
			string valid = "some-text";

			var exc = Record.Exception(() => parserType.ValidateParserData(valid));

			Assert.Null(exc);
		}
		#endregion

		#region ValidateParserData - CSV
		[Fact]
		public void ValidateData_ShouldThrowBusinessRuleValidationException_WhenTypeIsCsvAndInvalidColumnsAreReceived()
		{
			var parserType = ParserTypeBuilder.ForCsv();
			IEnumerable<string> invalid = [""];
			var rule = new ParserTypeCsvMustReceiveValidCsvPattern(invalid);

			var exc = Assert.Throws<BusinessRuleValidationException>(() => parserType.ValidateParserData(invalid));

			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateData_ShouldReturn_WhenTypeIsCsvAndValidColumnsAreReceived()
		{
			var parserType = ParserTypeBuilder.ForCsv();
			IEnumerable<string> valid = ["col1", "col2"];

			var exc = Record.Exception(() => parserType.ValidateParserData(valid));

			Assert.Null(exc);
		}
		#endregion
	}
}