using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Categories;
using Horus.Domain.Tooling.Manifests.Execution;
using Horus.Domain.Tooling.Manifests.Identity;
using Horus.Domain.Tooling.Manifests.Metadata;
using Horus.Domain.Tooling.Manifests.Parameters;
using Horus.Domain.Tooling.Manifests.Parsers;

namespace Horus.Domain.Tooling.Manifests
{
	public sealed class ToolManifest : Entity, IAggregateRoot
	{
		// Backing fields
		private readonly List<ToolParameter> _parameters = [];
		private readonly List<ToolCategory> _categories = [];

		public ManifestIdentity Identity { get; private set; } = default!;
		public ManifestMetadata Metadata { get; private set; } = default!;
		public ManifestExecution Execution { get; private set; } = default!;
		public IReadOnlyCollection<ToolParameter> Parameters => _parameters.AsReadOnly();
		public IReadOnlyCollection<ToolCategory> Categories => _categories.AsReadOnly();
		public ToolResultParser ResultParser { get; private set; } = default!;


		// Timestamps
		public DateTime CreatedAt { get; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

		// Relations


		// For EF
		[Obsolete("For EF Only", true)]
		private ToolManifest() { }

		private ToolManifest(
			ManifestIdentity identity,
			ManifestMetadata metadata,
			ManifestExecution execution,
			IEnumerable<ToolParameter> parameters,
			IEnumerable<ToolCategory> toolCategories,
			ToolResultParser resultParser
		)
		{
			Identity = identity;
			Metadata = metadata;
			Execution = execution;
			_parameters.AddRange(parameters);
			_categories.AddRange(toolCategories);
			ResultParser = resultParser;
		}

		public static ToolManifest Create(
			ManifestIdentity identity,
			ManifestMetadata metadata,
			ManifestExecution execution,
			IEnumerable<ToolParameter> parameters,
			ToolResultParser resultParser,
			IEnumerable<ToolCategory>? toolCategories = null
		)
		{
			return new ToolManifest(
				identity,
				metadata,
				execution,
				parameters,
				toolCategories ?? [],
				resultParser
			);
		}

		public void UpdateIdentity(ManifestIdentity identity)
		{
			Identity = identity;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateMetadata(ManifestMetadata metadata)
		{
			Metadata = metadata;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateExecution(ManifestExecution execution)
		{
			Execution = execution;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateParameters(IEnumerable<ToolParameter> parameters)
		{
			_parameters.Clear();
			_parameters.AddRange(parameters);
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateResultParser(ToolResultParser resultParser)
		{
			ResultParser = resultParser;
			UpdatedAt = DateTime.UtcNow;
		}

	}
}