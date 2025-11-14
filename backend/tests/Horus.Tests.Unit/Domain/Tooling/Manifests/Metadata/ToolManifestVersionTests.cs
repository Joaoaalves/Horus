
using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion;
using Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion.Rules;

namespace Horus.Tests.Unit.Domain.Tooling
{
	public class ToolManifestVersionTests
	{
		#region Invalid Creation Tests
		[Fact]
		public void ToolManifestVersion_ShouldThrowBusinessRuleValidationException_WhenVersionIsNotPositive()
		{
			// Arrange
			var invalidVersion = 0;
			var rule = new ToolManifestVersionMustBePositive(invalidVersion);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => ToolManifestVersion.Create(invalidVersion));
		}
		#endregion
		#region Valid Creation Tests
		[Fact]
		public void ToolManifestVersion_ShouldCreateSuccessfully_WhenVersionIsPositive()
		{
			// Arrange
			var validVersion = 1;
			// Act
			var toolManifestVersion = ToolManifestVersion.Create(validVersion);
			// Assert
			Assert.NotNull(toolManifestVersion);
			Assert.Equal(validVersion, toolManifestVersion.Value);
		}
		#endregion
		#region  Increment Tests
		[Fact]
		public void ToolManifestVersion_Increment_ShouldReturnNewVersionWithIncrementedValue()
		{
			// Arrange
			var initialVersion = ToolManifestVersion.Create(1);

			// Act
			var incrementedVersion = initialVersion.Increment();

			// Assert
			Assert.NotNull(incrementedVersion);
			Assert.Equal(initialVersion.Value + 1, incrementedVersion.Value);
		}
		#endregion
	}
}