using Horus.Domain.SeedWork;
using Horus.Domain.WordlistsManagement.WordlistCategories;
using Horus.Domain.WordlistsManagement.Wordlists;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.WordlistsManagement
{
	public class WordlistTests
	{
		[Fact]
		public void Wordlist_ShouldBeCreated_WhenValidParametersAreProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			string description = StringBuilder.Build(10);
			int size = 1000;

			// Act
			var wordlist = Wordlist.Create(name, size, description);

			// Assert
			Assert.NotNull(wordlist);
			Assert.Equal(name, wordlist.Name.Value);
			Assert.NotNull(wordlist.Description);
			Assert.Equal(description, wordlist.Description.Value);
			Assert.Contains(wordlist.Id.Value.ToString(), wordlist.FilePath.Value);
		}

		[Fact]
		public void Wordlist_ShouldBeCreated_WhenDescriptionIsNotProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;

			// Act
			var wordlist = Wordlist.Create(name, size);

			// Assert
			Assert.NotNull(wordlist);
			Assert.Equal(name, wordlist.Name.Value);
			Assert.Null(wordlist.Description);
			Assert.Contains(wordlist.Id.Value.ToString(), wordlist.FilePath.Value);
		}

		#region Wordlist Methods
		[Fact]
		public void Wordlist_TryUpdateDescription_ShouldUpdateDescription_WhenValidDescriptionIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			var wordlist = Wordlist.Create(name, size);

			string newDescription = StringBuilder.Build(20);

			// Act
			wordlist.TryUpdateDescription(newDescription);

			// Assert
			Assert.NotNull(wordlist.Description);
			Assert.Equal(newDescription, wordlist.Description.Value);
		}

		[Fact]
		public void Wordlist_TryUpdateDescription_ShouldNotUpdateDescription_WhenInvalidDescriptionIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			var wordlist = Wordlist.Create(name, size);
			string newDescription = StringBuilder.Build(301); // Invalid description

			// Act
			var exc = Record.Exception(() => wordlist.TryUpdateDescription(newDescription));

			// Assert
			Assert.Null(wordlist.Description);
			Assert.IsType<BusinessRuleValidationException>(exc);
		}

		[Fact]
		public void Wordlist_TryUpdateDescription_ShouldRemoveDescription_WhenNullIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			string description = StringBuilder.Build(20);
			var wordlist = Wordlist.Create(name, size, description);
			string? newDescription = null;

			// Act
			wordlist.TryUpdateDescription(newDescription);

			// Assert
			Assert.Null(wordlist.Description);
		}

		[Fact]
		public void Wordlist_TryUpdateDescription_ShouldNotUpdateDescription_WhenEmptyStringIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			string description = StringBuilder.Build(20);
			var wordlist = Wordlist.Create(name, size, description);
			string newDescription = string.Empty;

			// Act
			var exc = Record.Exception(() => wordlist.TryUpdateDescription(newDescription));

			// Assert
			Assert.NotNull(wordlist.Description);
			Assert.Equal(description, wordlist.Description.Value);
			Assert.IsType<BusinessRuleValidationException>(exc);
		}

		[Fact]
		public void Wordlist_Rename_ShouldUpdateName_WhenValidNameIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			var wordlist = Wordlist.Create(name, size);

			string newName = StringBuilder.Build(15);

			// Act
			wordlist.Rename(newName);

			// Assert
			Assert.Equal(newName, wordlist.Name.Value);
		}

		[Fact]
		public void Wordlist_Rename_ShouldNotUpdateName_WhenInvalidNameIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			var wordlist = Wordlist.Create(name, size);
			string newName = string.Empty; // Invalid name

			// Act
			var exc = Record.Exception(() => wordlist.Rename(newName));

			// Assert
			Assert.Equal(name, wordlist.Name.Value);
			Assert.IsType<BusinessRuleValidationException>(exc);
		}

		[Fact]
		public void Wordlist_UpdateCategory_ShouldUpdateCategory_WhenValidCategoryIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			var wordlist = Wordlist.Create(name, size);

			string categoryName = StringBuilder.Build(10);
			var category = WordlistCategory.Create(categoryName);

			// Act
			wordlist.UpdateCategory(category.Id);

			// Assert
			Assert.NotNull(wordlist.CategoryId);
			Assert.Equal(category.Id, wordlist.CategoryId);
		}

		[Fact]
		public void Wordlist_UpdateCategory_ShouldRemoveCategory_WhenNullIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			int size = 1000;
			string categoryName = StringBuilder.Build(10);
			var category = WordlistCategory.Create(categoryName);
			var wordlist = Wordlist.Create(name, size, categoryId: category.Id);

			// Act
			wordlist.UpdateCategory(null);

			// Assert
			Assert.Null(wordlist.Category);
		}

		#endregion
	}
}