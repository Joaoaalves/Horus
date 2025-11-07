using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts
{
	public sealed class ServiceFingerprint(string? name, string? product, string? version, string? vulnerability) : ValueObject
	{
		public string Name { get; } = name ?? string.Empty;
		public string Product { get; } = product ?? string.Empty;
		public string Version { get; } = version ?? string.Empty;
		public string Vulnerability { get; set; } = vulnerability ?? string.Empty;
	}
}