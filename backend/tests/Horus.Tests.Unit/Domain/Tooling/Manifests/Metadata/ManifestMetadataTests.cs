using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Metadata;
using Horus.Domain.Tooling.Manifests.Metadata.Categories;
using Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion.Rules;
using Horus.Domain.Tooling.Manifests.Metadata.SupportedServices.Rules;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Metadata
{
	public class ManifestMetadataTests
	{
		#region Invalid Creation Tests

		[Fact]
		public void ManifestMetadata_ShouldThrowBusinessRuleValidationException_WhenSupportedServicesAreEmpty()
		{
			// Arrange
			var version = 2;
			IEnumerable<string> validCategory = ["Security"];
			ManifestSource source = ManifestSource.Repo;
			IEnumerable<string> supportedServices = [];
			var rule = new SupportedServiceTypesCannotBeEmpty(supportedServices);

			// act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestMetadata.Create(
				version,
				validCategory,
				source,
				supportedServices
			));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestMetadata_ShouldThrowBusinessRuleValidationException_WhenVersionIsInvalid()
		{
			// Arrange
			var invalidVersion = -1;
			IEnumerable<string> validCategory = ["Security"];
			ManifestSource source = ManifestSource.Repo;
			IEnumerable<string> supportedServices = ["ServiceA"];
			var rule = new ToolManifestVersionMustBePositive(invalidVersion);

			// act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestMetadata.Create(
				invalidVersion,
				validCategory,
				source,
				supportedServices
			));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Creation Tests
		[Fact]
		public void ManifestMetadata_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			var validVersion = 1;
			IEnumerable<string> validCategories = ["Security", "Networking"];
			ManifestSource source = ManifestSource.Repo;
			IEnumerable<string> supportedServices = ["ServiceA", "ServiceB"];
			// Act
			var metadata = ManifestMetadata.Create(
				validVersion,
				validCategories,
				source,
				supportedServices
			);

			// Assert
			Assert.NotNull(metadata);
			Assert.Equal(validVersion, metadata.Version.Value);
			Assert.Equal(validCategories.Count(), metadata.Categories.Count);
			foreach (var category in validCategories)
			{
				ToolCategory toolCategory = ToolCategory.FromString(category);
				Assert.Contains(toolCategory, metadata.Categories);
			}
			Assert.Equal(source, metadata.Source);
			Assert.Equal(supportedServices.Count(), metadata.SupportedServices.ServiceTypes.Count);
		}
		#endregion
	}
}