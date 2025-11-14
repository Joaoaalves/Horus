using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Execution.Fakes
{
	public class FakeContainerImageVerifier(bool expectedResult) : IContainerImageVerifier
	{
		private readonly bool _expectedResult = expectedResult;
		public bool IsImageAvailable(string imageName)
		{
			return _expectedResult;
		}
	}
}