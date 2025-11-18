using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules
{
	public sealed class ParserTypeCannotAcceptNotSupportedType(HashSet<string> supportedTypes, string type) : IBusinessRule
	{
		// Class Body
		public string Message => $"Not supported parser type '{type}'.";

		public bool IsBroken() => !supportedTypes.Contains(type);
	}
}