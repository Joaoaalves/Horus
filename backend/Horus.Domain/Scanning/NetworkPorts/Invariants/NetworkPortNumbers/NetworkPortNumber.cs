using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortNumbers
{
	public sealed class NetworkPortNumber : ValueObject
	{
		public readonly uint Value;

		private NetworkPortNumber(uint value)
		{
			Value = value;
		}

		public static NetworkPortNumber Create(uint value)
		{
			CheckRule(new Rules.NetworkPortNumberMustBeInRange(value));

			return new NetworkPortNumber(value);
		}
	}
}