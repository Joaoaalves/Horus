using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Csv
{
	public sealed class ParserTypeCsvMustReceiveValidCsvPattern(object data) : IBusinessRule
	{
		public string Message => "CSV parser requires a valid CSV column selector list.";

		public bool IsBroken()
		{
			if (data is not IEnumerable<string> list)
				return true;

			if (!list.Any())
				return true;

			return list.Any(x => string.IsNullOrWhiteSpace(x));
		}
	}
}