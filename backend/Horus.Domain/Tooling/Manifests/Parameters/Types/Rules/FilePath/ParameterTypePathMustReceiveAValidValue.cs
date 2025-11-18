using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.FilePath
{
	public sealed class ParameterTypePathMustReceiveAValidValue(string value) : IBusinessRule
	{
		// Class Body
		public string Message => $"ParameterType: Expected path but received: {value}";

		public bool IsBroken() => value.IndexOfAny(Path.GetInvalidPathChars()) >= 0;
	}
}