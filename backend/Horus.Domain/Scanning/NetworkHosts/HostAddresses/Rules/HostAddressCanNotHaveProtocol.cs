using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkHosts.HostAddresses.Rules
{
	public sealed class HostAddressCanNotHaveProtocol(
		string address
	) : IBusinessRule
	{
		private readonly string _address = address;
		public string Message => "Host Address can't have scheme. You should remove 'http(s)://'.";

		public bool IsBroken() => _address.StartsWith("http://") || _address.StartsWith("https://");
	}
}