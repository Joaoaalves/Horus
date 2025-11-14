using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion
{
	public sealed class ToolManifestVersion : ValueObject
	{
		public int Value { get; }

		private ToolManifestVersion(int value)
		{
			Value = value;
		}

		public static ToolManifestVersion Create(int value)
		{
			CheckRule(new Rules.ToolManifestVersionMustBePositive(value));
			return new ToolManifestVersion(value);
		}

		public ToolManifestVersion Increment()
		{
			return new ToolManifestVersion(Value + 1);
		}
	}
}