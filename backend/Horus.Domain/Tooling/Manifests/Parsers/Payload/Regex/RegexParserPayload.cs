namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex
{
	public sealed class RegexParserPayload(IEnumerable<RegexPattern> patterns) : ParserPayload
	{
		public IReadOnlyCollection<RegexPattern> Patterns { get; } = patterns.ToList().AsReadOnly();

	}
}