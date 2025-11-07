using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts
{
	public sealed class NetworkPortId(Guid value) : TypedIdValueBase(value)
	{
		public NetworkPortId() : this(Guid.NewGuid()) { }
	}
}