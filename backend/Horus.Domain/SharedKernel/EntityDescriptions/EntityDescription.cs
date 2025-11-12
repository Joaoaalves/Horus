using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.EntityDescriptions
{
	public sealed class EntityDescription : ValueObject
	{
		public string Value { get; } = string.Empty;

		private EntityDescription(string value)
		{
			Value = value;
		}

		public static EntityDescription Create(string value)
		{
			CheckRule(new Rules.EntityDescriotionCannotBeNull(value));
			CheckRule(new Rules.EntityDescriptionLengthMustBeInRange(value));

			return new EntityDescription(value.Trim());
		}
	}
}