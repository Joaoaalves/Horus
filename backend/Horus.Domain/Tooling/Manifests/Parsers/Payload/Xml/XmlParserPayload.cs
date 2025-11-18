namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Xml
{
	public sealed class XmlParserPayload : ParserPayload
	{
		public string XPath { get; }

		public XmlParserPayload(string xPath)
		{
			XPath = xPath.Trim();
		}
	}
}