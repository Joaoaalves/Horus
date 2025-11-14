using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion.Rules
{
	public sealed class ToolManifestVersionMustBePositive(int version) : IBusinessRule
	{
		private readonly int _version = version;
		public string Message => "Tool manifest version must be a positive integer.";

		public bool IsBroken() => _version <= 0;
	}
}