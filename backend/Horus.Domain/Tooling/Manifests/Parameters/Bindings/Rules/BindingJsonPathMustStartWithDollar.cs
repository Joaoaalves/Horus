using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Bindings.Rules
{
	public sealed class BindingJsonPathMustStartWithDollar(string path) : IBusinessRule
	{
		public string Message => "The JSON path for the parameter binding must start with '$'.";

		public bool IsBroken() => !path.Trim().StartsWith('$');
	}
}