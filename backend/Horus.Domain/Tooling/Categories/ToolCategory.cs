using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.Tooling.Manifests;

namespace Horus.Domain.Tooling.Categories
{
	public sealed class ToolCategory : Entity
	{
		// Parameters
		public ToolCategoryId Id { get; } = default!;
		public EntityName Name { get; private set; } = default!;
		public EntityDescription Description { get; private set; } = default!;

		// Relations
		public IReadOnlyCollection<ToolManifest> Tools => _tools.AsReadOnly();

		// Backing Fields
		private readonly List<ToolManifest> _tools = [];

		[Obsolete("For EF Only", true)]
		private ToolCategory() { }

		private ToolCategory(ToolCategoryId id, EntityName name, EntityDescription description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public static ToolCategory Create(string name, string description)
		{
			return new ToolCategory(
				new ToolCategoryId(),
				EntityName.FromString(name),
				EntityDescription.FromString(description)
			);
		}

		public void AddTool(ToolManifest tool)
		{
			if (tool is null || _tools.Contains(tool))
				return;

			_tools.Add(tool);
		}

		public void RemoveTool(ToolManifest tool)
		{
			_tools.Remove(tool);
		}

		public void Rename(string newName)
		{
			var name = EntityName.FromString(newName);

			Name = name;
		}

		public void UpdateDescription(string newDescription)
		{
			var description = EntityDescription.FromString(newDescription);

			Description = description;
		}
	}
}