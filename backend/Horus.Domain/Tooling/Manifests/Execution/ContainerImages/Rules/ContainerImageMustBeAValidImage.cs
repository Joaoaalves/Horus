using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Execution.ContainerImages.Rules
{
	public sealed class ContainerImageMustBeAValidImage(string imageName, IContainerImageVerifier imageVerifier) : IBusinessRule
	{
		public string Message => $"The container image '{imageName}' is not a valid image or is not accessible.";

		public bool IsBroken() => !imageVerifier.IsImageAvailable(imageName);
	}
}