using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Json
{
	public sealed class ParserTypeMustReceiveValidJsonPath(object data) : IBusinessRule
	{

		public string Message => "JsonPath parser must receive a valid JSONPath string.";

		public bool IsBroken()
		{
			if (data is not string str)
				return true;

			if (string.IsNullOrWhiteSpace(str))
				return true;

			return !str.TrimStart().StartsWith('$');
		}
	}
}