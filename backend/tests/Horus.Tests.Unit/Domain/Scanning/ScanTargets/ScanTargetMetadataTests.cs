using Horus.Domain.Scanning.ScanTargets.ScanTargetMetadatas;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Scanning.ScanTargets
{
	public class ScanTargetMetadataTests
	{
		[Fact]
		public void ScanTargetMetadata_ShouldThrowBusinessRuleValidation_WhenDescriptionIsNull()
		{
			// Arrange
			string? value = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetMetadata.Create(value!));
		}

		[Fact]
		public void ScanTargetMetadata_ShouldThrowBusinessRuleValidation_WhenDescriptionIsEmpty()
		{
			// Arrange
			string value = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetMetadata.Create(value));
		}
		[Fact]
		public void ScanTargetMetadata_ShouldThrowBusinessRuleValidation_WhenDescriptionIsLowerThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(1);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetMetadata.Create(value));
		}

		[Fact]
		public void ScanTargetMetadata_ShouldThrowBusinessRuleValidation_WhenDescriptionIsGreatherThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(301);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetMetadata.Create(value));
		}

		[Fact]
		public void ScanTargetMetadata_ShouldBeCreated_WhenDescriptionIsOnBoundaries()
		{
			// Arrange
			string value = StringBuilder.Build(100); // Valid length

			// Act
			var stMetadata = ScanTargetMetadata.Create(value);

			// Assert
			Assert.Equal(value, stMetadata.Description);
		}

		[Fact]
		public void ScanTargetMetadata_ShouldBeCreated_WhenDescriptionHasSpecialCharacters()
		{
			// Arrange
			string value = StringBuilder.Build(100, true); // Valid length with special chars

			// Act
			var stMetadata = ScanTargetMetadata.Create(value);

			// Assert
			Assert.Equal(value, stMetadata.Description);
		}
	}
}