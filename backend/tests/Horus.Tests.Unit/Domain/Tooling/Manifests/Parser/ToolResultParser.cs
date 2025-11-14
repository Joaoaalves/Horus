using Horus.Domain.Tooling.Manifests.Parsers;
using Horus.Domain.Tooling.Manifests.Parsers.RegexPatterns;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parser
{
	public class ToolResultParserTests
	{
		#region Valid Constructor Cases
		[Fact]
		public void ToolResultParser_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			string type = "valid type"; // Temporary
			IEnumerable<RegexPattern> patterns = [RegexPattern.Create("valid type", "regex", "critical")];
			string outputPath = "/valid/path";

			// Act
			var parser = ToolResultParser.Create(type, patterns, outputPath);

			// Assert
			Assert.NotNull(parser);
			Assert.Equal(type, parser.Type);
			Assert.Equal(outputPath, parser.OutputPath.Value);
			Assert.Equal(patterns, parser.Patterns);
		}
		#endregion
	}
}