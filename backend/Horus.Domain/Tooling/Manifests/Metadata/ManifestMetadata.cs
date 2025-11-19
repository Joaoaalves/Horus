using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Metadata.Categories;
using Horus.Domain.Tooling.Manifests.Metadata.ManifestVersion;
using Horus.Domain.Tooling.Manifests.Metadata.SupportedServices;

namespace Horus.Domain.Tooling.Manifests.Metadata
{
	public sealed class ManifestMetadata : ValueObject
	{
		public readonly List<ToolCategory> _categories = [];

		public ManifestSource Source { get; } = default!;
		public SupportedServiceTypes SupportedServices { get; } = default!;
		public ToolManifestVersion Version { get; } = default!;

		public IReadOnlyCollection<ToolCategory> Categories => _categories.AsReadOnly();

		private ManifestMetadata(IEnumerable<ToolCategory> categories, ManifestSource source, SupportedServiceTypes supportedServices, ToolManifestVersion version)
		{
			Source = source;
			SupportedServices = supportedServices;
			Version = version;
			_categories.AddRange(categories);
		}


		public static ManifestMetadata Create(int version, IEnumerable<string> categories, ManifestSource source, IEnumerable<string> supportedServices)
		{
			var categoryList = categories.Select(cat => ToolCategory.FromString(cat));

			return new ManifestMetadata(
				categoryList,
				source,
				SupportedServiceTypes.FromList(supportedServices),
				ToolManifestVersion.Create(version)
			);
		}
	}
}