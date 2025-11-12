using Horus.Domain.WordlistsManagement.WordlistCategories;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.WordlistsManagement
{
	public class WordlistCategoryTests
	{
		[Fact]
		public void WordlistCategory_ShouldBeCreated_WhenValidNameIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);

			// Act
			var category = WordlistCategory.Create(name);

			// Assert
			Assert.NotNull(category);
			Assert.Equal(name, category.Name.Value);
		}
	}
}