using Horus.Domain.SeedWork;

namespace Horus.Domain.WordlistsManagement.Wordlists
{
	public sealed class WordlistId(Guid value) : TypedIdValueBase(value)
	{
		public WordlistId() : this(Guid.NewGuid())
		{
		}
	}
}