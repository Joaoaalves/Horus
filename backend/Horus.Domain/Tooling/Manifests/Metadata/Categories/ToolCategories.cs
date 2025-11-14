using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Metadata.Categories
{
	public sealed class ToolCategory : ValueObject
	{
		public string Value { get; }

		private ToolCategory(string value) => Value = value;

		public static ToolCategory FromString(string value)
		{
			CheckRule(new StringCannotBeEmptyOrNull(value, nameof(ToolCategory)));
			return new ToolCategory(value.Trim());
		}
	}
}