using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Vulnerabilities.Evidences
{
	public sealed class Evidence : ValueObject
	{
		public string Value { get; } = default!;
		private Evidence(string value)
		{
			Value = value;
		}

		public static Evidence Create(string value)
		{
			CheckRule(new Rules.EvidenceCannotBeNullOrEmpty(value));
			CheckRule(new Rules.EvidenceMustBeAValidJson(value));

			return new Evidence(value.Trim());
		}

		public T ToObject<T>()
		{
			return System.Text.Json.JsonSerializer.Deserialize<T>(Value)!;
		}
	}
}