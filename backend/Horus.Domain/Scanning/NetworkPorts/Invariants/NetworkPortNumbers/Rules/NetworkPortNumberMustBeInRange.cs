using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortNumbers.Rules
{
	public class NetworkPortNumberMustBeInRange(uint portNumber) : IBusinessRule
	{
		private const uint MinPortNumber = 1;
		private const uint MaxPortNumber = 65535;

		public uint PortNumber { get; } = portNumber;

		public string Message => $"Port number must be between {MinPortNumber} and {MaxPortNumber}.";

		public bool IsBroken() => PortNumber < MinPortNumber || PortNumber > MaxPortNumber;
	}
}