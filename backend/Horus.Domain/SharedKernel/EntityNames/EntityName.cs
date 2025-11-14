using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.SharedKernel.EntityNames
{
	public sealed class EntityName : ValueObject
	{
		public string Value { get; }

		private EntityName(string value)
		{
			Value = value;
		}

		public static EntityName FromString(string value)
		{
			CheckRule(new StringCannotBeEmptyOrNull(value, nameof(EntityName)));

			CheckRule(new Rules.EntityNameLengthMustBeInRange(value));

			return new EntityName(value);
		}

		public override string ToString() => Value;
	}
}