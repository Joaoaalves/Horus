namespace Horus.Domain.Tooling.Manifests.Execution.ContainerImages
{
	public interface IContainerImageVerifier
	{
		bool IsImageAvailable(string imageName);
	}
}