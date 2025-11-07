using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas.Rules
{
	public sealed class NetworkPortMetadataJsonShouldNotBeEmpty(string metadata) : IBusinessRule
	{
		private readonly string _metadata = metadata;
		public string Message => "Network Port Metadata JSON should not be empty or null.";

		public bool IsBroken() => string.IsNullOrEmpty(_metadata);
	}
}