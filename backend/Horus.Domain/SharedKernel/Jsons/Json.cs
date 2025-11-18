using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.SharedKernel.Jsons
{
	public sealed class Json : ValueObject
	{
		public string Value { get; } = string.Empty;

		private Json(string value)
		{
			Value = value;
		}

		public static Json FromString(string json)
		{
			CheckRule(new StringCannotBeEmptyOrNull(json, nameof(Json)));
			CheckRule(new Rules.JsonMustBeValid(json));

			return new Json(json);
		}
	}
}