using Horus.Domain.SeedWork;

namespace Horus.Domain.Notes.Findings
{
	public sealed class FindingId(Guid value) : TypedIdValueBase(value)
	{
		public FindingId() : this(Guid.NewGuid()) { }
	}
}