using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types
{
	public sealed class ParameterType : ValueObject
	{
		public string Value { get; }

		private static readonly HashSet<string> PrimitiveTypes = [
			"string",
			"number",
			"boolean",
			"url",
			"path"
		];

		private readonly List<string>? _enumValues;

		private ParameterType(string value, List<string>? enumValues = null)
		{
			Value = value;
			_enumValues = enumValues;
		}

		public static ParameterType Create(string rawType)
		{
			CheckRule(new StringCannotBeEmptyOrNull(rawType, "ToolParameters.ParameterType"));

			var type = rawType.Trim().ToLowerInvariant();

			if (type.StartsWith("enum:"))
			{
				var values = type.Substring(5)
					.Split(",", StringSplitOptions.RemoveEmptyEntries)
					.Select(v => v.Trim())
					.ToList();

				CheckRule(new Rules.Enum.EnumOptionsCannotBeEmpty(values));

				return new ParameterType("enum", values);
			}

			CheckRule(new Rules.ParameterTypeCannotAcceptNotSupportedType(PrimitiveTypes, type));

			return new ParameterType(type);
		}

		public void ValidateValue(string? value)
		{
			if (value is null || Value == "string")
				return;

			if (Value == "number")
				CheckRule(new Rules.Number.ParameterTypeNumberMustReceiveAValidNumber(value));

			if (Value == "boolean")
				CheckRule(new Rules.Boolean.ParameterTypeBooleanMustReceiveAValidValue(value));

			if (Value == "url")
				CheckRule(new Rules.Url.ParameterTypeUrlMustReceiveAValidValue(value));

			if (Value == "path")
				CheckRule(new Rules.FilePath.ParameterTypePathMustReceiveAValidValue(value));

			if (Value == "enum")
				CheckRule(new Rules.Enum.EnumValueMustBeValid(_enumValues, value.Trim()));

		}
	}
}