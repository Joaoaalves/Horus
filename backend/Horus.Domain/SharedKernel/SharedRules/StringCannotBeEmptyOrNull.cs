using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.SharedRules
{
	public sealed class StringCannotBeEmptyOrNull(string value, string field) : IBusinessRule
	{
		public string Message => $"{field} cannot be null or empty.";
		public bool IsBroken() => string.IsNullOrWhiteSpace(value);
	}
}