using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Url
{
	public sealed class ParameterTypeUrlMustReceiveAValidValue(string value) : IBusinessRule
	{
		public string Message => $"ParameterType: Expected url but received: {value}";

		public bool IsBroken()
		{
			var urlPattern = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

			return !System.Text.RegularExpressions.Regex.IsMatch(value, urlPattern);
		}
	}
}