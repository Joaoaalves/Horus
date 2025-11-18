namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex
{
	public sealed class RegexParserPayload(IEnumerable<RegexPattern> patterns) : ParserPayload
	{
		public IReadOnlyList<RegexPattern> Patterns { get; } = patterns.ToList();

	}
}