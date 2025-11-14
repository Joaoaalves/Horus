using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parameters.Bindings;

namespace Horus.Domain.Tooling.Manifests.Parameters
{
	public sealed class ToolParameter : ValueObject
	{
		public string Name { get; }
		public string Type { get; }
		public bool Required { get; } = false;
		public string? DefaultValue { get; }
		public ParameterBinding? Binding { get; }
		public string? Description { get; }

		private ToolParameter(
				string name,
				string type,
				bool required,
				string? defaultValue,
				ParameterBinding? binding,
				string? description)
		{
			Name = name;
			Type = type;
			Required = required;
			DefaultValue = defaultValue;
			Binding = binding;
			Description = description;
		}

		public static ToolParameter Create(
			string name,
			string type,
			bool required,
			string? defaultValue = null,
			ParameterBinding? binding = null,
			string? description = null)
		{
			CheckRule(new StringCannotBeEmptyOrNull(name, "ToolParameter.Name"));
			CheckRule(new StringCannotBeEmptyOrNull(type, "ToolParameter.Type"));

			return new ToolParameter(
				name.Trim(),
				type.Trim(),
				required,
				defaultValue?.Trim(),
				binding,
				description?.Trim()
			);
		}

	}
}