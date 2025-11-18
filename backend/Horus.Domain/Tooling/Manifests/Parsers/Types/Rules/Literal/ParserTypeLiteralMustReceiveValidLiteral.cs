using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parsers.Types.Rules.Literal
{
	public sealed class ParserTypeLiteralMustReceiveValidLiteral(object data) : IBusinessRule
	{
		public string Message => "Literal parser requires a non-empty string literal.";

		public bool IsBroken()
		{
			if (data is not string str)
				return true;

			return string.IsNullOrWhiteSpace(str);
		}
	}
}