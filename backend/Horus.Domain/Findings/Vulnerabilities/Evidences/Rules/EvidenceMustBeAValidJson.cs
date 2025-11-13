using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Vulnerabilities.Evidences.Rules
{
	public sealed class EvidenceMustBeAValidJson(string evidence) : IBusinessRule
	{
		private readonly string _evidence = evidence;

		public string Message => "The evidence must be a valid JSON.";

		public bool IsBroken()
		{
			try
			{
				System.Text.Json.JsonDocument.Parse(_evidence);
				return false;
			}
			catch (System.Text.Json.JsonException)
			{
				return true;
			}
		}
	}
}