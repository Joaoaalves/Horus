using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.Jsons.Rules
{
	public sealed class JsonMustBeValid(string json) : IBusinessRule
	{
		// Class Body
		public string Message => "Invalid Json format.";

		public bool IsBroken()
		{
			try
			{
				System.Text.Json.JsonDocument.Parse(json);
				return false;
			}
			catch (System.Text.Json.JsonException)
			{
				return true;
			}
		}
	}
}