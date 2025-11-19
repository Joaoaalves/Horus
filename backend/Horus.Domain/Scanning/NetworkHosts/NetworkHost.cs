using Horus.Domain.Findings.Notes;
using Horus.Domain.Scanning.NetworkHosts.HostAddresses;
using Horus.Domain.Scanning.NetworkPorts;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Domain.SharedKernel.EntityNames;

namespace Horus.Domain.Scanning.NetworkHosts
{
	public sealed class NetworkHost : AnnotableEntity
	{
		// Backing Fields
		private readonly List<NetworkPort> _ports = [];

		// Attributes
		public NetworkHostId Id { get; } = default!;
		public EntityName Name { get; private set; } = default!;
		public EntityDescription? Description { get; private set; } = default!;
		public HostAddress Address { get; private set; } = default!;

		// Relations
		public IReadOnlyCollection<NetworkPort> Ports => _ports.AsReadOnly();

		// For EF
		[Obsolete("For EF Only", true)]
		private NetworkHost() { }

		private NetworkHost(NetworkHostId id, EntityName name, HostAddress address, EntityDescription? description = null)
		{
			Id = id;
			Name = name;
			Description = description;
			Address = address;
		}

		public static NetworkHost Create(string name, string address, string? description = null)
		{
			var hostName = EntityName.FromString(name);
			var hostDesc = description is not null
				? EntityDescription.FromString(description.Trim())
				: null;
			var hostAddr = HostAddress.Create(address);

			return new NetworkHost(
				new NetworkHostId(),
				hostName,
				hostAddr,
				hostDesc
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