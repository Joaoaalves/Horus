namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Json
{
	public sealed class JsonParserPayload : ParserPayload
	{
		public string JsonPath { get; }

		private JsonParserPayload(string jsonPath)
		{
			JsonPath = jsonPath;
		}
	}
}