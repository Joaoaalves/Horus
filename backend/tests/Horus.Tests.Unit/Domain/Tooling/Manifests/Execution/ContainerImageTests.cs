using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages.Rules;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Execution.Fakes;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Execution
{
	public class ContainerImageTests
	{
		#region Invalid Cases
		[Fact]
		public void ContainerImage_ShouldThrowBusinessRuleValidationException_WhenImageNameIsEmpty()
		{
			// Arrange
			string emptyImageName = "";
			var rule = new StringCannotBeEmptyOrNull(emptyImageName, nameof(ContainerImage));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ContainerImage.Create(
				emptyImageName, new FakeContainerImageVerifier(false)));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ContainerImage_ShouldThrowBusinessRuleValidationException_WhenImageNameIsNull()
		{
			// Arrange
			string? nullImageName = null;
			var rule = new StringCannotBeEmptyOrNull(nullImageName!, nameof(ContainerImage));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ContainerImage.Create(
				nullImageName!, new FakeContainerImageVerifier(false)));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ContainerImage_ShouldThrowBusinessRuleValidationException_WhenImageDoesNotExist()
		{
			// Arrange
			FakeContainerImageVerifier fakeVerifier = new(false);
			string invalidImageName = "nonexistent/image:latest";
			var rule = new ContainerImageMustBeAValidImage(invalidImageName, fakeVerifier);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ContainerImage.Create(
				invalidImageName, fakeVerifier));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Cases
		[Fact]
		public void ContainerImage_ShouldCreateSuccessfully_WhenValidImageNameIsProvided()
		{
			// Arrange
			FakeContainerImageVerifier fakeVerifier = new(true);
			string validImageName = "valid/image:latest";

			// Act
			var containerImage = ContainerImage.Create(
				validImageName, fakeVerifier);

			// Assert
			Assert.NotNull(containerImage);
			Assert.Equal(validImageName, containerImage.Value);
		}
		#endregion
	}
}