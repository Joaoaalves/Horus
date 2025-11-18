using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Enum
{
	public sealed class EnumValueMustBeValid(IEnumerable<string>? enumValues, string value) : IBusinessRule
	{
		// Class Body
		public string Message => $"Invalid enum option '{value}'. Allowed: {string.Join(", ", enumValues!)}";

		public bool IsBroken() => enumValues is null || !enumValues.Contains(value);
	}
}