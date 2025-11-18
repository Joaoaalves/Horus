using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex
{
	public sealed class RegexPattern : ValueObject
	{
		public string Name { get; }
		public string Regex { get; }
		public string Severity { get; }

		private RegexPattern(string name, string regex, string severity)
		{
			Name = name;
			Regex = regex;
			Severity = severity;
		}

		public static RegexPattern Create(string name, string regex, string severity)
		{
			CheckRule(new StringCannotBeEmptyOrNull(name, "RegexPattern.Name"));
			CheckRule(new StringCannotBeEmptyOrNull(regex, "RegexPattern.Regex"));
			CheckRule(new StringCannotBeEmptyOrNull(severity, "RegexPattern.Severity"));

			return new RegexPattern(name.Trim(), regex.Trim(), severity.Trim());
		}

	}
}