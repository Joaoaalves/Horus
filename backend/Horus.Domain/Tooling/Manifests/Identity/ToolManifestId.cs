using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Identity
{
	public sealed class ToolManifestId(Guid value) : TypedIdValueBase(value)
	{
		public ToolManifestId() : this(Guid.NewGuid())
		{
		}
	}
}