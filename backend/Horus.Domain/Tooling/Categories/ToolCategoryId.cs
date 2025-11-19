using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Categories
{
	public sealed class ToolCategoryId(Guid value) : TypedIdValueBase(value)
	{
		public ToolCategoryId() : this(Guid.NewGuid())
		{
		}
	}
}