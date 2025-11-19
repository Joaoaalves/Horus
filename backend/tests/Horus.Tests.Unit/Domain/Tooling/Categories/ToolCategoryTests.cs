using Horus.Domain.Tooling.Categories;
using Horus.Domain.Tooling.Manifests;
using Horus.Tests.Unit.Domain.Tooling.Categories.Builders;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Builders;

namespace Horus.Tests.Unit.Domain.Tooling
{
	public class ToolCategoryTests
	{
		#region Creation Cases
		[Fact]
		public void Create_ShouldReturnAValidToolCategory_WhenReceivedValidParameters()
		{
			// Arrange
			string name = "Name";
			string description = "Description";

			// Act
			var category = ToolCategory.Create(name, description);

			// Assert
			Assert.NotNull(category);
			Assert.Equal(name, category.Name.Value);
			Assert.Equal(description, category.Description.Value);
		}
		#endregion

		#region AddTool
		[Fact]
		public void AddTool_ShouldReturn_WhenNullToolIsProvided()
		{
			// Arrange
			ToolManifest? manifest = null;
			var category = ToolCategoryBuilder.Build();

			// Act
			Assert.Empty(category.Tools);
			var exc = Record.Exception(() => category.AddTool(manifest!));

			// Assert
			Assert.Null(exc);
			Assert.Empty(category.Tools);
		}

		[Fact]
		public void AddTool_ShouldAddTool_WhenValidToolIsProvided()
		{
			// Arrange
			var manifest = ToolManifestBuilder.Build();
			var category = ToolCategoryBuilder.Build();

			// Act
			Assert.Empty(category.Tools);
			var exc = Record.Exception(() => category.AddTool(manifest));

			// Assert
			Assert.Null(exc);
			Assert.NotEmpty(category.Tools);
			Assert.Equal(manifest, category.Tools.First());
		}

		[Fact]
		public void AddTool_ShouldntAddDuplicatedTool()
		{
			// Arrange
			var manifest = ToolManifestBuilder.Build();
			var category = ToolCategoryBuilder.Build();

			// Act
			category.AddTool(manifest);
			Assert.Equal(manifest, category.Tools.First());
			var exc = Record.Exception(() => category.AddTool(manifest));

			// Assert
			Assert.Null(exc);
			Assert.NotEmpty(category.Tools);
			Assert.Single(category.Tools);
		}
		#endregion

		#region RemoveTool
		[Fact]
		public void RemoveTool_ShouldReturn_WhenNullToolIsProvided()
		{
			// Arrange
			ToolManifest? nullManifest = null;
			var category = ToolCategoryBuilder.Build();

			// Act
			Assert.Empty(category.Tools);
			var exc = Record.Exception(() => category.RemoveTool(nullManifest!));

			// Assert
			Assert.Null(exc);
			Assert.Empty(category.Tools);
		}

		[Fact]
		public void RemoveTool_ShouldRemoveTool_WhenValidToolIsProvided()
		{
			// Arrange
			var manifest = ToolManifestBuilder.Build();
			var category = ToolCategoryBuilder.Build();

			// Act
			Assert.Empty(category.Tools);
			category.AddTool(manifest);
			Assert.Single(category.Tools);
			var exc = Record.Exception(() => category.RemoveTool(manifest));

			// Assert
			Assert.Null(exc);
			Assert.Empty(category.Tools);
		}

		[Fact]
		public void RemoveTool_ShouldReturn_WhenToolIsNotPresent()
		{
			// Arrange
			var addedManifest = ToolManifestBuilder.Build();
			var notAddedManifest = ToolManifestBuilder.Build();
			var category = ToolCategoryBuilder.Build();

			// Act
			category.AddTool(addedManifest);
			Assert.Equal(addedManifest, category.Tools.First());
			var exc = Record.Exception(() => category.RemoveTool(notAddedManifest));

			// Assert
			Assert.Null(exc);
			Assert.NotEmpty(category.Tools);
			Assert.Single(category.Tools);
			Assert.Equal(addedManifest, category.Tools.First());
		}
		#endregion

		#region Rename
		[Fact]
		public void Rename_ShouldRenameCategory_WhenValidNameIsProvided()
		{
			// Arrange
			string name = "First name";
			var category = ToolCategoryBuilder.Build(name);

			// Act
			Assert.Equal(name, category.Name.Value);
			string newName = "Updated Name";
			category.Rename(newName);

			// Assert
			Assert.Equal(newName, category.Name.Value);
		}
		#endregion

		#region UpdateDescription
		[Fact]
		public void UpdateDescription_ShouldRenameCategory_WhenValidNameIsProvided()
		{
			// Arrange
			string description = "First description";
			var category = ToolCategoryBuilder.Build(description: description);

			// Act
			Assert.Equal(description, category.Description.Value);
			string newDescription = "Updated description";
			category.UpdateDescription(newDescription);

			// Assert
			Assert.Equal(newDescription, category.Description.Value);
		}
		#endregion
	}
}