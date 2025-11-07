using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkHosts.HostAddresses.Rules
{
	public class HostAddressMustBeIpOrUrl(string address) : IBusinessRule
	{
		public string Message => "Host address must be a valid IP address or URL.";
		public bool IsBroken()
		{
			var ipPattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
			var urlPattern = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

			return !System.Text.RegularExpressions.Regex.IsMatch(address, ipPattern) &&
				   !System.Text.RegularExpressions.Regex.IsMatch(address, urlPattern);
		}

	}
}