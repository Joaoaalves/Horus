using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkHosts
{
	public sealed class NetworkHostId(Guid value) : TypedIdValueBase(value)
	{
		public NetworkHostId() : this(Guid.NewGuid()) { }
	}
}