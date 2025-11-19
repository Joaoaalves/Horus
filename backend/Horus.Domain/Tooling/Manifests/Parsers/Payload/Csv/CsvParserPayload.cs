namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Csv
{
	public sealed class CsvParserPayload : ParserPayload
	{
		public char Separator { get; }
		public IReadOnlyCollection<string> Columns { get; }

		public CsvParserPayload(char separator, IEnumerable<string> columns)
		{
			Separator = separator;
			Columns = columns.Select(c => c.Trim()).ToList();
		}
	}

}