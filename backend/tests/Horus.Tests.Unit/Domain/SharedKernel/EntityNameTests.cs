using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.SharedKernel
{
	public class EntityNameTests
	{
		[Fact]
		public void EntityName_ShouldThrowBusinessRuleValidationException_WhenValueIsNull()
		{
			// Arrange
			string? value = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityName.FromString(value!));
		}

		[Fact]
		public void EntityName_ShouldThrowBusinessRuleValidationException_WhenValueIsEmpty()
		{
			// Arrange
			string value = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityName.FromString(value));
		}

		[Fact]
		public void EntityName_ShouldThrowBusinessRuleValidationException_WhenValueIsLowerThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(1);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityName.FromString(value));
		}

		[Fact]
		public void EntityName_ShouldThrowBusinessRuleValidationException_WhenValueIsGreatherThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(101);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => EntityName.FromString(value));
		}

		[Fact]
		public void EntityName_ShouldCreateWithSuccess_WhenValueIsValid()
		{
			// Arrange
			string value = StringBuilder.Build(10);

			// Act
			var stName = EntityName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void EntityName_ShouldCreateWithSuccess_WhenLengthIsAtLowerBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(3);

			// Act
			var stName = EntityName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void EntityName_ShouldCreateWithSuccess_WhenLengthIsAtUpperBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(100);

			// Act
			var stName = EntityName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void EntityName_ShouldCreateWithSuccess_WhenValueHasSpecialCharacters()
		{
			// Arrange
			string value = StringBuilder.Build(10, true); // Generate random string with special characters

			// Act
			var stName = EntityName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}
	}
}