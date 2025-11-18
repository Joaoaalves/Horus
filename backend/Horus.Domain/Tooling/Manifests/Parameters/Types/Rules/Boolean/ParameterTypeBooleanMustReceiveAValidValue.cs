using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Boolean
{
	public sealed class ParameterTypeBooleanMustReceiveAValidValue(string value) : IBusinessRule
	{
		public string Message => $"ParameterType: Expected boolean but received: {value}";

		public bool IsBroken() => !bool.TryParse(value, out _);
	}
}