using System.Globalization;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Number
{
	public sealed class ParameterTypeNumberMustReceiveAValidNumber(string value) : IBusinessRule
	{
		// Class Body
		public string Message => $"ParameterType: Expected number but received: {value}";

		public bool IsBroken() => !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
	}
}