using System.Text.RegularExpressions;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkHosts.Invariants.HostAddress
{
	public sealed class HostAddress : ValueObject
	{
		public string Value { get; }

		private HostAddress(string value)
		{
			Value = value;
		}

		public static HostAddress Create(string address)
		{
			CheckRule(new Rules.HostAddressCanNotBeNullOrEmpty(address));

			var trimmed = address.TrimEnd('/');
			var clean = RemoveProtocol(trimmed);

			CheckRule(new Rules.HostAddressMustBeIpOrUrl(clean));
			CheckRule(new Rules.HostAddressCanNotHaveProtocol(clean));

			return new HostAddress(clean);
		}

		private static string RemoveProtocol(string address)
		{
			string pattern = @"^\s*h+t+p+s*\s*:\s*/+\s*";

			return Regex.Replace(address, pattern, "", RegexOptions.IgnoreCase);
		}

		// Ensure the address has a scheme (http by default). Does not validate host reachability.
		// Example: "example.com" -> "http://example.com"
		//          "https://example.com/path" -> "https://example.com/path"
		public string WithScheme(string defaultScheme = "http")
		{
			var scheme = defaultScheme.Trim().TrimEnd(':', '/');
			return $"${scheme}://{Value}";
		}

		// Combine base address (ensuring scheme) with an arbitrary path segment.
		// This does minimal normalization: strips duplicate slashes and leading/trailing slashes from segment.
		// Caller is responsible for placeholders like FUZZ or more advanced path logic.
		// Examples:
		//   CombinePath("api/v1") => "http://example.com/api/v1"
		//   CombinePath("/api/v1/") => "http://example.com/api/v1"
		public string CombinePath(string pathSegment, string defaultScheme = "http")
		{
			ArgumentNullException.ThrowIfNull(pathSegment);

			var baseWithScheme = WithScheme(defaultScheme).TrimEnd('/');
			var cleaned = pathSegment.Trim();

			cleaned = cleaned.Trim('/');

			if (string.IsNullOrEmpty(cleaned))
				return baseWithScheme;

			return $"{baseWithScheme}/{cleaned}";
		}

		// Convenience: return a Uri for code that prefers Uri.
		public Uri ToUri(string defaultScheme = "http")
			=> new(WithScheme(defaultScheme));
	}
}