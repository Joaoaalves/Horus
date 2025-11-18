using Horus.Domain.Tooling.Manifests.Parameters.Types;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters.Builders
{
	public static class ParameterTypeBuilder
	{
		public static ParameterType ForNumber() => ParameterType.Create("number");
		public static ParameterType ForEnum(IEnumerable<string> options) => ParameterType.Create($"enum:{string.Join(",", options)}");
		public static ParameterType ForString() => ParameterType.Create("string");
		public static ParameterType ForUrl() => ParameterType.Create("url");
		public static ParameterType ForPath() => ParameterType.Create("path");
		public static ParameterType ForBoolean() => ParameterType.Create("boolean");
	}
}