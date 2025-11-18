using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Csv;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Json;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Kv;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Lines;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Xml;
using Horus.Domain.Tooling.Manifests.Parsers.Types;

namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Rules
{
	public sealed class ParserPayloadMustMatchParserType : IBusinessRule
	{
		private readonly ParserType _type;
		private readonly ParserPayload _payload;

		public ParserPayloadMustMatchParserType(ParserType type, ParserPayload payload)
		{
			_type = type;
			_payload = payload;
		}

		public string Message => $"Parser payload does not match parser type '{_type.Value}'.";

		public bool IsBroken()
		{
			return _type.Value switch
			{
				"regex" => _payload is not RegexParserPayload,
				"json" => _payload is not JsonParserPayload,
				"xml" => _payload is not XmlParserPayload,
				"lines" => _payload is not LinesParserPayload,
				"csv" => _payload is not CsvParserPayload,
				"kv" => _payload is not KeyValueParserPayload,
				_ => true
			};
		}
	}
}