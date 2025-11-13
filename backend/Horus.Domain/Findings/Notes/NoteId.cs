using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Notes
{
	public sealed class NoteId(Guid value) : TypedIdValueBase(value)
	{
		public NoteId() : this(Guid.NewGuid()) { }
	}
}