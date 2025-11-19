using Horus.Domain.Findings.Notes;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;
using Moq;

namespace Horus.Tests.Unit.Domain.Scanning.ScanTargets
{
	public class ScanTargetTests
	{
		#region  invalid construct cases
		[Fact]
		public void ScanTarget_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? name = null;

			// Act & Arrange
			Assert.Throws<BusinessRuleValidationException>(() => ScanTarget.Create(name!));
		}

		[Fact]
		public void ScanTarget_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string name = string.Empty;

			// Act & Arrange
			Assert.Throws<BusinessRuleValidationException>(() => ScanTarget.Create(name));
		}
		#endregion

		#region  valid construct cases
		[Fact]
		public void ScanTarget_ShouldBeCreated_WhenDescriptionIsNotProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);

			// Act
			var target = ScanTarget.Create(name);

			// Assert
			Assert.NotNull(target);
			Assert.Equal(name, target.Name.Value);
			Assert.Null(target.Metadata);
		}

		[Fact]
		public void ScanTarget_ShouldBeCreated_WhenDescriptionIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			string description = StringBuilder.Build(10);


			// Act
			var target = ScanTarget.Create(name, description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.NotNull(target.Metadata.Description);
			Assert.Equal(description, target.Metadata.Description.Value);
		}
		#endregion

		#region Rename
		[Fact]
		public void Rename_ShouldRenameScanTarget_WhenAValidNameIsReceived()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			// Act
			string renameTo = StringBuilder.Build(10);
			target.Rename(renameTo);

			// Assert
			Assert.Equal(renameTo, target.Name.Value);
		}
		#endregion

		#region UpdateMetadata
		[Fact]
		public void UpdateMetadata_ShouldUpdateMetadataCorrectly_WhenValidDataIsReceived()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			string firstDescription = StringBuilder.Build(10);
			var target = ScanTarget.Create(name, firstDescription);

			// Act
			string description = StringBuilder.Build(10);
			target.UpdateMetadata(description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.NotNull(target.Metadata.Description);
			Assert.Equal(description, target.Metadata.Description.Value);
		}

		[Fact]
		public void UpdateMetadata_ShouldUpdateMetadata_WhenItWasEmpty()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			// Act
			string description = StringBuilder.Build(10);
			target.UpdateMetadata(description);

			// Assert
			Assert.NotNull(target.Metadata);
			Assert.NotNull(target.Metadata.Description);
			Assert.Equal(description, target.Metadata.Description.Value);
		}
		#endregion

		#region IAnnotable - AddNote
		[Fact]
		public void AddNote_ShouldReturn_WhenNullNoteIsProvided()
		{
			// Arrange
			Note? note = null;
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			// Act
			var exc = Record.Exception(() => target.AddNote(note!));

			// Assert
			Assert.Null(exc);
			Assert.Empty(target.Notes);
		}

		[Fact]
		public void AddNote_ShouldAddNote_WhenValidNoteIsProvided()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);

			// Act
			var exc = Record.Exception(() => target.AddNote(note));

			// Assert
			Assert.Null(exc);
			Assert.Single(target.Notes);
		}

		[Fact]
		public void AddNote_ShouldAddAnotherNote_WhenNotesAreNoteEmpty()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);
			Note note2 = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);

			// Act
			target.AddNote(note);
			Assert.Single(target.Notes);
			target.AddNote(note2);

			// Assert
			Assert.Equal(2, target.Notes.Count);
			Assert.Contains(note2, target.Notes);
		}

		[Fact]
		public void AddNote_ShouldntAddAnotherNote_WhenNotesAreEqualEmpty()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);

			// Act
			target.AddNote(note);
			Assert.Single(target.Notes);
			target.AddNote(note);

			// Assert
			Assert.Single(target.Notes);
		}
		#endregion
		#region IAnnotable - Remove Note
		[Fact]
		public void RemoveNote_ShouldRemoveNote_WhenNoteIsPresent()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);
			target.AddNote(note);

			// Act
			Assert.Single(target.Notes);
			var exc = Record.Exception(() => target.RemoveNote(note));

			// Assert
			Assert.Null(exc);
			Assert.Empty(target.Notes);
		}

		[Fact]
		public void RemoveNote_ShouldReturn_WhenNoteIsNotPresent()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);
			Note note2 = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);
			target.AddNote(note);

			// Act
			Assert.Single(target.Notes);
			var exc = Record.Exception(() => target.RemoveNote(note2));

			// Assert
			Assert.Null(exc);
			Assert.Single(target.Notes);
		}

		[Fact]
		public void RemoveNote_ShouldReturn_WhenNoteIsNull()
		{
			// Arrange
			string name = StringBuilder.Build(10);
			var target = ScanTarget.Create(name);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForScanTarget(noteTile, target.Id, fakeNotePathHandler.Object);
			Note? note2 = null;
			target.AddNote(note);

			// Act
			Assert.Single(target.Notes);
			var exc = Record.Exception(() => target.RemoveNote(note2!));

			// Assert
			Assert.Null(exc);
			Assert.Single(target.Notes);
		}
		#endregion
	}
}