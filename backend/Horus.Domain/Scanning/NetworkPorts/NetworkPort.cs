using Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas;
using Horus.Domain.Scanning.NetworkPorts.NetworkPortNumbers;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts
{
	public sealed class NetworkPort : Entity
	{
		// Attributes
		public NetworkPortId Id { get; } = default!;
		public NetworkPortNumber Number { get; private set; } = default!;
		public NetworkPortStatus Status { get; private set; } = NetworkPortStatus.Closed;
		public Protocol Protocol { get; private set; }
		public ServiceFingerprint? Service { get; private set; }
		public NetworkPortMetadata Metadata { get; private set; } = default!;

		[Obsolete("For EF only", true)]
		private NetworkPort() { }

		private NetworkPort(
			NetworkPortId id,
			NetworkPortNumber number,
			Protocol protocol,
			NetworkPortStatus status = NetworkPortStatus.Closed,
			ServiceFingerprint? service = null
		)
		{
			Id = id;
			Number = number;
			Protocol = protocol;
			Status = status;
			Service = service;
			Metadata = NetworkPortMetadata.Empty();
		}

		public static NetworkPort Create(
			uint portNumber,
			Protocol protocol,
			NetworkPortStatus status = NetworkPortStatus.Closed,
			ServiceFingerprint? service = null
		)
		{
			var networkPortNumber = NetworkPortNumber.Create(portNumber);

			return new NetworkPort(
				new NetworkPortId(),
				networkPortNumber,
				protocol,
				status,
				service
			);
		}

		public void UpdateMetadata(NetworkPortMetadata metadata)
		{
			Metadata = metadata;
		}
	}
}