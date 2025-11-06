using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Scanning.Targets
{
	public class ScanTargetTests
	{
		[Fact]
		public void ScanTarget_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? name = null;

			// Act & Arrange
			Assert.Throws<BusinessRuleValidationException>(() => ScanTarget.Create(name!));
		}

		[Fact]
		public void ScanTarget_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string name = string.Empty;

			// Act & Arrange
			Assert.Throws<BusinessRuleValidationException>(() => ScanTarget.Create(name));
		}

		[Fact]
		public void ScanTarget_ShouldBeCreated_WhenDescriptionIsNotProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);

			// Act
			var target = ScanTarget.Create(name);

			// Assert
			Assert.NotNull(target);
			Assert.Equal(name, target.Name.Value);
			Assert.Null(target.Metadata);
		}

		[Fact]
		public void ScanTarget_ShouldBeCreated_WhenDescriptionIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			string description = StringBuilder.Build(10);


			// Act
			var target = ScanTarget.Create(name, description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.Equal(description, target.Metadata.Description);
		}

		[Fact]
		public void ScanTarget_ShouldBeRenamed_WhenAValidNameIsReceived()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			// Act
			string renameTo = StringBuilder.Build(10);
			target.Rename(renameTo);

			// Assert
			Assert.Equal(renameTo, target.Name.Value);
		}

		[Fact]
		public void ScanTarget_ShouldUpdateMetadata_WhenAValidDescriptionIsReceived()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			string firstDescription = StringBuilder.Build(10);
			var target = ScanTarget.Create(name, firstDescription);

			// Act
			string description = StringBuilder.Build(10);
			target.UpdateMetadata(description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.Equal(description, target.Metadata.Description);
		}

		[Fact]
		public void ScanTarget_ShouldUpdateMetadata_WhenItWasEmpty()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			// Act
			string description = StringBuilder.Build(10);
			target.UpdateMetadata(description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.Equal(description, target.Metadata.Description);
		}
	}
}