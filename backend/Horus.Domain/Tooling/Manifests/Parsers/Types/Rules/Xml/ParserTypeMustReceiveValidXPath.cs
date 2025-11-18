using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Xml
{
	public sealed class ParserTypeMustReceiveValidXPath(object data) : IBusinessRule
	{

		public string Message => "XPath parser must receive a valid XPath expression.";

		public bool IsBroken()
		{
			if (data is not string str)
				return true;

			if (string.IsNullOrWhiteSpace(str))
				return true;

			return !(str.StartsWith("/") || str.StartsWith("//"));
		}
	}
}