using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.SharedKernel
{
	public class EntityDescriptionTests
	{
		[Fact]
		public void EntityDescription_ShouldThrowBusinessRuleValidation_WhenDescriptionIsNull()
		{
			// Arrange
			string? value = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityDescription.Create(value!));
		}

		[Fact]
		public void EntityDescription_ShouldThrowBusinessRuleValidation_WhenDescriptionIsEmpty()
		{
			// Arrange
			string value = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityDescription.Create(value));
		}
		[Fact]
		public void EntityDescription_ShouldThrowBusinessRuleValidation_WhenDescriptionIsLowerThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(1);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityDescription.Create(value));
		}

		[Fact]
		public void EntityDescription_ShouldThrowBusinessRuleValidation_WhenDescriptionIsGreatherThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(301);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityDescription.Create(value));
		}

		[Fact]
		public void EntityDescription_ShouldBeCreated_WhenDescriptionIsOnBoundaries()
		{
			// Arrange
			string value = StringBuilder.Build(100); // Valid length

			// Act
			var description = EntityDescription.Create(value);

			// Assert
			Assert.Equal(value, description.Value);
		}

		[Fact]
		public void EntityDescription_ShouldBeCreated_WhenDescriptionHasSpecialCharacters()
		{
			// Arrange
			string value = StringBuilder.Build(100, true); // Valid length with special chars

			// Act
			var description = EntityDescription.Create(value);

			// Assert
			Assert.Equal(value, description.Value);
		}
	}
}