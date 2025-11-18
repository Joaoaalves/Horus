using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.FilePaths;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parsers.Payload;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Rules;
using Horus.Domain.Tooling.Manifests.Parsers.Types;

namespace Horus.Domain.Tooling.Manifests.Parsers
{
	public sealed class ToolResultParser : ValueObject
	{
		public ParserType Type { get; }
		public ParserPayload Payload { get; }
		public FilePath OutputPath { get; }

		private ToolResultParser(
			ParserType type,
			ParserPayload payload,
			FilePath outputPath)
		{
			Type = type;
			Payload = payload;
			OutputPath = outputPath;
		}

		public static ToolResultParser Create(
			string type,
			ParserPayload payload,
			string outputPath)
		{
			CheckRule(new StringCannotBeEmptyOrNull(type, "ToolResultParser.Type"));
			CheckRule(new StringCannotBeEmptyOrNull(outputPath, "ToolResultParser.OutputPath"));

			var parserType = ParserType.Create(type.Trim());

			CheckRule(new ParserPayloadMustMatchParserType(parserType, payload));

			return new ToolResultParser(
				parserType,
				payload,
				FilePath.FromString(outputPath)
			);
		}
	}
}