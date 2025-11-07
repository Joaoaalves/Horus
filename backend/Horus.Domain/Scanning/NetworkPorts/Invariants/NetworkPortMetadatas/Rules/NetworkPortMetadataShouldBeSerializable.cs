using System.Text.Json;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas.Rules
{
	public sealed class NetworkPortMetadataShouldBeSerializable(object metadata) : IBusinessRule
	{
		private readonly object _metadata = metadata;
		public string Message => "Metadata Object should be serializable.";

		public bool IsBroken()
		{
			try
			{
				JsonSerializer.Serialize(_metadata);
				return false;
			}
			catch
			{
				return true;
			}
		}
	}
}