using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.SharedKernel.FilePaths;
using Horus.Domain.WordlistsManagement.WordlistCategories;
using Horus.Domain.WordlistsManagement.Wordlists.WordlistSizes;

namespace Horus.Domain.WordlistsManagement.Wordlists
{
	public sealed class Wordlist : Entity, IAggregateRoot
	{
		// Attributes
		public WordlistId Id { get; private init; } = default!;
		public EntityName Name { get; private set; } = default!;
		public EntityDescription? Description { get; private set; } = default!;
		public WordlistSize Size { get; private set; } = default!;
		public FilePath FilePath { get; private set; } = default!;

		// Relations
		public WordlistCategory? Category { get; private set; }
		public WordlistCategoryId? CategoryId { get; private set; }

		// For EF Only
		[Obsolete("For EF Only", true)]
		private Wordlist() { }

		private Wordlist(WordlistId id, EntityName name, WordlistSize size, FilePath filePath, EntityDescription? description = null, WordlistCategoryId? categoryId = null)
		{
			Id = id;
			Name = name;
			Size = size;
			FilePath = filePath;
			Description = description;
			CategoryId = categoryId;
		}

		public static Wordlist Create(string name, int wordsCount, string? description = null, WordlistCategoryId? categoryId = null)
		{
			var wordlistId = new WordlistId();
			var wordlistName = EntityName.FromString(name);
			var wordlistSize = WordlistSize.Create(wordsCount);
			var wordlistPath = FilePath.FromString($"/wordlists/{wordlistId.Value}.txt");
			EntityDescription? wordlistDescription = null;

			if (description is not null)
			{
				wordlistDescription = EntityDescription.FromString(description.Trim());
			}

			return new Wordlist(
				wordlistId,
				wordlistName,
				wordlistSize,
				wordlistPath,
				wordlistDescription,
				categoryId
			);
		}

		public void UpdateSize(int wordsCount)
		{
			var wordlistSize = WordlistSize.Create(wordsCount);
			Size = wordlistSize;
		}

		public void TryUpdateDescription(string? description)
		{
			if (description is null)
			{
				Description = null;
				return;
			}

			var wordlistDescription = EntityDescription.FromString(description.Trim());
			Description = wordlistDescription;
		}

		public void UpdateCategory(WordlistCategoryId? categoryId)
		{
			CategoryId = categoryId;
		}

		public void Rename(string name)
		{
			var wordlistName = EntityName.FromString(name);
			Name = wordlistName;
		}
	}
}