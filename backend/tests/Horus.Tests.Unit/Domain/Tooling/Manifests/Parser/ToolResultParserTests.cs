using Horus.Domain.Tooling.Manifests.Parsers;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parser
{
	public class ToolResultParserTests
	{
		#region Valid Constructor Cases
		[Fact]
		public void ToolResultParser_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			string type = "regex";
			IEnumerable<RegexPattern> patterns = [RegexPattern.Create("valid type", "regex", "critical")];
			RegexParserPayload payload = new(patterns);
			string outputPath = "/valid/path";

			// Act
			var parser = ToolResultParser.Create(type, payload, outputPath);

			// Assert
			Assert.NotNull(parser);
			Assert.Equal(type, parser.Type.Value);
			Assert.Equal(outputPath, parser.OutputPath.Value);
		}
		#endregion
	}
}