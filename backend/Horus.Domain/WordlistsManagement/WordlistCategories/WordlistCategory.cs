using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.WordlistsManagement.Wordlists;

namespace Horus.Domain.WordlistsManagement.WordlistCategories
{
	public sealed class WordlistCategory : Entity
	{
		// Backing Field
		private readonly List<WordlistId> _wordlists = [];

		// Attributes
		public WordlistCategoryId Id { get; } = default!;
		public EntityName Name { get; private set; } = default!;
		public EntityDescription? Description { get; private set; } = default!;

		// Relations
		public IReadOnlyCollection<WordlistId> Wordlists => _wordlists.AsReadOnly();

		// For EF
		[Obsolete("For EF Only", true)]
		private WordlistCategory() { }

		private WordlistCategory(WordlistCategoryId id, EntityName name, EntityDescription? description = null)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public static WordlistCategory Create(string name, string? description = null)
		{
			var categoryName = EntityName.FromString(name);
			EntityDescription? categoryDescription = null;

			if (description is not null)
			{
				categoryDescription = EntityDescription.Create(description.Trim());
			}

			return new WordlistCategory(
				new WordlistCategoryId(),
				categoryName,
				categoryDescription
			);
		}
	}
}