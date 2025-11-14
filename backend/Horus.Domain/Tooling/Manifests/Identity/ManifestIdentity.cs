using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Domain.SharedKernel.EntityNames;

namespace Horus.Domain.Tooling.Manifests.Identity
{
	public sealed class ManifestIdentity : ValueObject
	{
		public ToolManifestId Id { get; private set; } = default!;
		public EntityName Name { get; private set; } = default!;
		public EntityDescription? Description { get; private set; } = default!;

		private ManifestIdentity(
			ToolManifestId id,
			EntityName name,
			EntityDescription? description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public static ManifestIdentity Create(
			string name,
			string? description = null)
		{
			var manifestName = EntityName.FromString(name);
			var manifestDesc = description is not null
				? EntityDescription.Create(description.Trim())
				: null;

			return new ManifestIdentity(
				new ToolManifestId(),
				manifestName,
				manifestDesc
			);
		}

		public void UpdateDescription(string? description)
		{
			if (description is null)
			{
				Description = null;
			}
			else
			{
				Description = EntityDescription.Create(description.Trim());
			}

		}

		public void Rename(string name)
		{
			var manifestName = EntityName.FromString(name);
			Name = manifestName;
		}

	}
}