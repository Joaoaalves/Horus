using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Enum
{
	public sealed class EnumOptionsCannotBeEmpty(IEnumerable<string> values) : IBusinessRule
	{
		public string Message => "Parameter values for type 'Enum' cannot be empty";

		public bool IsBroken() => !values.Any();
	}
}