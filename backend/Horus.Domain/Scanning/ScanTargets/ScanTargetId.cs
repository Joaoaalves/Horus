using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets
{
	public sealed class ScanTargetId(Guid value) : TypedIdValueBase(value)
	{
		public ScanTargetId() : this(Guid.NewGuid()) { }
	}
}