using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Regex
{
	public sealed class ParserTypeMustReceiveValidRegexList(object data) : IBusinessRule
	{
		public string Message =>
			"Regex parser requires a non-empty list of RegexPattern.";

		public bool IsBroken()
		{
			if (data is not IEnumerable<RegexPattern> list)
				return true;

			return !list.Any();
		}
	}
}