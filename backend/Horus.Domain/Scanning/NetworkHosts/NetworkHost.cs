using Horus.Domain.Scanning.NetworkHosts.HostAddresses;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames;

namespace Horus.Domain.Scanning.NetworkHosts
{
	public sealed class NetworkHost : Entity
	{
		// Backing Fields

		// Attributes
		public NetworkHostId Id { get; } = default!;
		public EntityName Name { get; private set; } = default!;
		public HostAddress Address { get; private set; } = default!;

		// For EF
		[Obsolete("For EF Only", true)]
		private NetworkHost() { }

		private NetworkHost(NetworkHostId id, EntityName name, HostAddress address)
		{
			Id = id;
			Name = name;
			Address = address;
		}

		public static NetworkHost Create(string name, string address)
		{
			var hostName = EntityName.FromString(name);
			var hostAddr = HostAddress.Create(address);

			return new NetworkHost(
				new NetworkHostId(),
				hostName,
				hostAddr
			);
		}

		public void Rename(string name)
		{
			var hostName = EntityName.FromString(name);
			Name = hostName;
		}

		public void UpdateAddress(string address)
		{
			var hostAddr = HostAddress.Create(address);
			Address = hostAddr;
		}
	}
}