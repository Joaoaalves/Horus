using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Execution;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages.Rules;
using Horus.Domain.Tooling.Manifests.Execution.Templates;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Execution.Fakes;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Execution
{
	public class ManifestExecutionTests
	{
		#region Invalid Creation Cases
		[Fact]
		public void ManifestExecution_ShouldThrowBusinessRuleValidationException_WhenCommandTemplateIsEmpty()
		{
			// Arrange
			FakeContainerImageVerifier fakeVerifier = new(true);
			ContainerImage validImage = ContainerImage.Create("valid/image:latest", fakeVerifier);
			string emptyCommandTemplate = "";
			var rule = new StringCannotBeEmptyOrNull(emptyCommandTemplate, nameof(CommandTemplate));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestExecution.Create(
				validImage,
				emptyCommandTemplate
			));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ManifestExecution_ShouldThrowBusinessRuleValidationException_WhenCommandTemplateIsNull()
		{
			// Arrange
			FakeContainerImageVerifier fakeVerifier = new(true);
			ContainerImage validImage = ContainerImage.Create("valid/image:latest", fakeVerifier);
			string? nullCommandTemplate = null;
			var rule = new StringCannotBeEmptyOrNull(nullCommandTemplate!, nameof(CommandTemplate));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ManifestExecution.Create(
				validImage,
				nullCommandTemplate!
			));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Creation Case
		[Fact]
		public void ManifestExecution_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			FakeContainerImageVerifier fakeVerifier = new(true);
			ContainerImage validImage = ContainerImage.Create("valid/image:latest", fakeVerifier);
			string validCommandTemplate = "echo 'Hello, World!'";

			// Act
			var manifestExecution = ManifestExecution.Create(
				validImage,
				validCommandTemplate
			);

			// Assert
			Assert.NotNull(manifestExecution);
			Assert.Equal(validImage, manifestExecution.Image);
			Assert.Equal(validCommandTemplate, manifestExecution.CommandTemplate.Value);
		}
		#endregion
	}
}