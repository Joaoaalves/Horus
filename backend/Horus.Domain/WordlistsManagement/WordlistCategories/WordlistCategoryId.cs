using Horus.Domain.SeedWork;

namespace Horus.Domain.WordlistsManagement.WordlistCategories
{
	public sealed class WordlistCategoryId(Guid value) : TypedIdValueBase(value)
	{
		public WordlistCategoryId() : this(Guid.NewGuid())
		{
		}
	}
}