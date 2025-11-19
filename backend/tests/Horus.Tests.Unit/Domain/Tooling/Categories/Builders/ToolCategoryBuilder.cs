using Horus.Domain.Tooling.Categories;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Tooling.Categories.Builders
{
	public static class ToolCategoryBuilder
	{
		public static ToolCategory Build(string? name = null, string? description = null)
		{
			if (string.IsNullOrWhiteSpace(name))
				name = StringBuilder.Build(10);

			if (string.IsNullOrWhiteSpace(description))
				description = StringBuilder.Build(10);

			return ToolCategory.Create(name, description);
		}
	}
}