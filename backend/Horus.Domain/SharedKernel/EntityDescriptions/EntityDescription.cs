using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.SharedKernel.EntityDescriptions
{
	public sealed class EntityDescription : ValueObject
	{
		public string Value { get; } = string.Empty;

		private EntityDescription(string value)
		{
			Value = value;
		}

		public static EntityDescription FromString(string value)
		{
			CheckRule(new StringCannotBeEmptyOrNull(value, nameof(EntityDescription)));
			CheckRule(new Rules.EntityDescriptionLengthMustBeInRange(value));

			return new EntityDescription(value.Trim());
		}
	}
}