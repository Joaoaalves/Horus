using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Parameters.Bindings
{
	public sealed class ParameterBinding : ValueObject
	{
		public string Entity { get; }
		public string JsonPath { get; }

		private ParameterBinding(string entity, string jsonPath)
		{
			Entity = entity;
			JsonPath = jsonPath;
		}

		public static ParameterBinding Create(string entity, string jsonPath)
		{
			CheckRule(new StringCannotBeEmptyOrNull(entity, "ParameterBinding.Entity"));
			CheckRule(new StringCannotBeEmptyOrNull(jsonPath, "ParameterBinding.JsonPath"));
			CheckRule(new Rules.BindingJsonPathMustStartWithDollar(jsonPath));

			return new ParameterBinding(entity.Trim(), jsonPath.Trim());
		}
	}
}