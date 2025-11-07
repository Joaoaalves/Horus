using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkHosts.HostAddresses.Rules
{
	public sealed class HostAddressCanNotBeNullOrEmpty(
		string address
	) : IBusinessRule
	{
		private readonly string _address = address;
		public string Message => "Host Address Can't be null or empty.";

		public bool IsBroken() => string.IsNullOrEmpty(_address);
	}
}