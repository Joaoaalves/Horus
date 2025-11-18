namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Lines
{
	public sealed class LinesParserPayload : ParserPayload
	{
		public string? StartsWith { get; }
		public string? Contains { get; }
		public string? EndsWith { get; }

		public LinesParserPayload(string? startsWith, string? contains, string? endsWith)
		{
			StartsWith = startsWith?.Trim();
			Contains = contains?.Trim();
			EndsWith = endsWith?.Trim();
		}
	}

}