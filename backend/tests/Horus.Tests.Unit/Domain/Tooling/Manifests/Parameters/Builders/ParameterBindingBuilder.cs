using Horus.Domain.Tooling.Manifests.Parameters.Bindings;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters.Builders
{
	public static class ParameterBindingBuilder
	{
		public static ParameterBinding Build()
		{
			return ParameterBinding.Create(
				"ScanTarget",
				"$.name"
			);
		}
	}
}