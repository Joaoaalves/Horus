using System.Text.Json.Nodes;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas.Rules
{
	public sealed class NetworkPortMetadataShouldBeAValidJson(string metadata) : IBusinessRule
	{
		private readonly string _metadata = metadata;
		public string Message => "Network Port Metadata should be a valid JSON.";

		public bool IsBroken()
		{
			try
			{
				JsonNode.Parse(_metadata);
				return false;
			}
			catch
			{
				return true;
			}
		}
	}
}