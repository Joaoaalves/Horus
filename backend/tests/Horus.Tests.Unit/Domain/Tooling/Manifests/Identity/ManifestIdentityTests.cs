using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions.Rules;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.SharedKernel.EntityNames.Rules;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Identity;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Identity
{
	public class ManifestIdentityTests
	{
		#region Invalid Creation Tests
		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? nullName = null;
			var rule = new StringCannotBeEmptyOrNull(nullName!, nameof(EntityName));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create(nullName!));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string emptyName = string.Empty;
			var rule = new StringCannotBeEmptyOrNull(emptyName, nameof(EntityName));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create(emptyName));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenNameIsShorterThanMinLength()
		{
			// Arrange
			string shortName = StringBuilder.Build(2);
			var rule = new EntityNameLengthMustBeInRange(shortName);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create(shortName));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenNameIsLongerThanMaxLength()
		{
			// Arrange
			string longName = StringBuilder.Build(101);

			var rule = new EntityNameLengthMustBeInRange(longName);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create(longName));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenDescriptionIsShorterThanMinLength()
		{
			// Arrange
			string shortDescription = StringBuilder.Build(4);
			var rule = new EntityDescriptionLengthMustBeInRange(shortDescription);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create("Valid Name", shortDescription));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestIdentity_ShouldThrowBusinessRuleValidationException_WhenDescriptionIsLongerThanMaxLength()
		{
			// Arrange
			string longDescription = StringBuilder.Build(501);
			var rule = new EntityDescriptionLengthMustBeInRange(longDescription);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestIdentity.Create("Valid Name", longDescription));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Creation Tests
		[Fact]
		public void ManifestIdentity_ShouldCreateSuccessfully_WhenValidNameAndDescriptionAreProvided()
		{
			// Arrange
			string validName = "Valid Manifest Name";
			string validDescription = "This is a valid description for the manifest.";

			// Act
			var manifestIdentity = ManifestIdentity.Create(validName, validDescription);
			// Assert
			Assert.NotNull(manifestIdentity);
			Assert.Equal(validName, manifestIdentity.Name.Value);
			Assert.NotNull(manifestIdentity.Description);
		}

		[Fact]
		public void ManifestIdentity_ShouldCreateSuccessfully_WhenOnlyValidNameIsProvided()
		{
			// Arrange
			string validName = "Valid Manifest Name";

			// Act
			var manifestIdentity = ManifestIdentity.Create(validName);

			// Assert
			Assert.NotNull(manifestIdentity);
			Assert.Equal(validName, manifestIdentity.Name.Value);
			Assert.Null(manifestIdentity.Description);
		}
		#endregion
	}
}