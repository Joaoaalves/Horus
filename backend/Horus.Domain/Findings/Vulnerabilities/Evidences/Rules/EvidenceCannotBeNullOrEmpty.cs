using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Vulnerabilities.Evidences.Rules
{
	public sealed class EvidenceCannotBeNullOrEmpty(string evidence) : IBusinessRule
	{
		private readonly string _evidence = evidence;
		public string Message => "Evidence cannot be null or empty.";

		public bool IsBroken() => string.IsNullOrWhiteSpace(_evidence);
	}
}