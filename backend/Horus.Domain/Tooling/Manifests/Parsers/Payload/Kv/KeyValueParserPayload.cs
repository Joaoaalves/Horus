namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Kv
{
	public sealed class KeyValueParserPayload : ParserPayload
	{
		public string Separator { get; }

		public KeyValueParserPayload(string separator)
		{
			Separator = separator.Trim();
		}
	}

}