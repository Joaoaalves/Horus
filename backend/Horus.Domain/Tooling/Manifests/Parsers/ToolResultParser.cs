using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.FilePaths;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parsers.RegexPatterns;

namespace Horus.Domain.Tooling.Manifests.Parsers
{
	public sealed class ToolResultParser : ValueObject
	{
		public string Type { get; }
		public IReadOnlyList<RegexPattern> Patterns { get; }
		public FilePath OutputPath { get; }

		private ToolResultParser(
			string type, IEnumerable<RegexPattern> patterns,
			FilePath outputPath
		)
		{
			Type = type;
			Patterns = patterns.ToList();
			OutputPath = outputPath;
		}

		public static ToolResultParser Create(
			string type,
			IEnumerable<RegexPattern> patterns,
			string outputPath
		)
		{
			CheckRule(new StringCannotBeEmptyOrNull(
				type, "ToolResultParser.Type"
			));

			CheckRule(new StringCannotBeEmptyOrNull(
				outputPath, "ToolResultParser.OutputPath"
			));

			return new ToolResultParser(
				type.Trim(),
				patterns,
				FilePath.FromString(outputPath)
			);
		}
	}
}