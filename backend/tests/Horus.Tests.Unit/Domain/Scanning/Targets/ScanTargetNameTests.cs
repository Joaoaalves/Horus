using Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetName;
using Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetName.Rules;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Scanning.ScanTargets
{
	public class ScanTargetNameTests
	{
		[Fact]
		public void ScanTargetName_ShouldThrowBusinessRuleValidationException_WhenValueIsNull()
		{
			// Arrange
			string? value = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetName.FromString(value!));
		}

		[Fact]
		public void ScanTargetName_ShouldThrowBusinessRuleValidationException_WhenValueIsEmpty()
		{
			// Arrange
			string value = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetName.FromString(value));
		}

		[Fact]
		public void ScanTargetName_ShouldThrowBusinessRuleValidationException_WhenValueIsLowerThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(1);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetName.FromString(value));
		}

		[Fact]
		public void ScanTargetName_ShouldThrowBusinessRuleValidationException_WhenValueIsGreatherThanBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(101);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ScanTargetName.FromString(value));
		}

		[Fact]
		public void ScanTargetName_ShouldCreateWithSuccess_WhenValueIsValid()
		{
			// Arrange
			string value = StringBuilder.Build(10);

			// Act
			var stName = ScanTargetName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void ScanTargetName_ShouldCreateWithSuccess_WhenLengthIsAtLowerBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(3);

			// Act
			var stName = ScanTargetName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void ScanTargetName_ShouldCreateWithSuccess_WhenLengthIsAtUpperBoundary()
		{
			// Arrange
			string value = StringBuilder.Build(100);

			// Act
			var stName = ScanTargetName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}

		[Fact]
		public void ScanTargetName_ShouldCreateWithSuccess_WhenValueHasSpecialCharacters()
		{
			// Arrange
			string value = StringBuilder.Build(10, true); // Generate random string with special characters

			// Act
			var stName = ScanTargetName.FromString(value);

			// Assert
			Assert.Equal(value, stName.Value);
		}
	}
}