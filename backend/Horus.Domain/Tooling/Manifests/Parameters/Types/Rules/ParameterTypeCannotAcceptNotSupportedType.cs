using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules
{
	public sealed class ParameterTypeCannotAcceptNotSupportedType(HashSet<string> supportedTypes, string type) : IBusinessRule
	{
		// Class Body
		public string Message => $"Parameter type: '{type}' is not supported";

		public bool IsBroken() => !supportedTypes.Contains(type);
	}
}