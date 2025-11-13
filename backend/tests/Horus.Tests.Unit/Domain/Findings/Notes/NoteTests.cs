using Horus.Domain.Findings.Notes;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames.Rules;
using Horus.Tests.Unit.Builders;
using Moq;

namespace Horus.Tests.Unit.Domain.Findings.Notes
{
	public class NoteTests
	{

		#region Creation with invalid state

		[Fact]
		public static void Note_ShouldThrowBusinessRuleValidationException_WhenTitleIsEmpty()
		{
			// Arrange
			string title = string.Empty;
			ScanTargetId id = new();
			var rule = new EntityNameCanNotBeNull(title);
			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act & Arrange
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Note.ForScanTarget(title, id, fakeNotePathHandler.Object));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public static void Note_ShouldThrowBusinessRuleValidationException_WhenTitleIsNull()
		{
			// Arrange
			string? title = null;
			ScanTargetId id = new();
			var rule = new EntityNameCanNotBeNull(title!);

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act & Arrange
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Note.ForScanTarget(title!, id, fakeNotePathHandler.Object));
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region ForNetworkHost

		[Fact]
		public static void Note_ForNetworkHost_ShouldReturnANoteWithSameNetworkHostId()
		{
			// Arrange
			string title = "Valid Title!";
			NetworkHostId hostId = new();

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act
			var note = Note.ForNetworkHost(title, hostId, fakeNotePathHandler.Object);

			// Assert
			Assert.NotNull(note);
			Assert.Equal(hostId, note.NetworkHostId);
			Assert.Null(note.ScanTargetId);
		}

		#endregion

		#region ForScanTarget

		[Fact]
		public static void Note_ForScanTarget_ShouldReturnANoteWithSameScanTargetId()
		{
			// Arrange
			string title = "Valid Title!";
			ScanTargetId scanTargetId = new();

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act
			var note = Note.ForScanTarget(title, scanTargetId, fakeNotePathHandler.Object);

			// Assert
			Assert.NotNull(note);
			Assert.Equal(scanTargetId, note.ScanTargetId);
			Assert.Null(note.NetworkHostId);
		}

		#endregion

		#region UpdateTitle
		[Fact]
		public static void Note_UpdateTitle_ShouldUpdateNoteTitleCorrectly()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act
			var note = Note.ForScanTarget(title, scanTargetId, fakeNotePathHandler.Object);

			// Act & Assert
			Assert.Equal(title, note.Title.Value);

			string newTitle = "New Title!";
			note.UpdateTitle(newTitle);
			Assert.Equal(newTitle, note.Title.Value);
		}

		[Fact]
		public static void Note_UpdateTitle_ShouldThrowBusinessRuleValidationException_WhenTitleIsInvalid()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act
			var note = Note.ForScanTarget(title, scanTargetId, fakeNotePathHandler.Object);

			// Act & Assert
			Assert.Equal(title, note.Title.Value);

			string shortTitle = StringBuilder.Build(1);
			Assert.Throws<BusinessRuleValidationException>(() =>
				note.UpdateTitle(shortTitle)
			);

			string longTitle = StringBuilder.Build(200);
			Assert.Throws<BusinessRuleValidationException>(() =>
				note.UpdateTitle(longTitle)
			);

			string emptyTitle = string.Empty;
			Assert.Throws<BusinessRuleValidationException>(() =>
				note.UpdateTitle(emptyTitle)
			);
		}

		#endregion

		#region MarkUpdated
		[Fact]
		public void Note_MarkUpdated_ShouldCorrectlySetUpdatedAtValue()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeNotePathHandler = new Mock<INotePathHandler>();

			// Act
			var note = Note.ForScanTarget(title, scanTargetId, fakeNotePathHandler.Object);
			var firstUpdatedAt = note.UpdatedAt;
			note.MarkUpdated();

			// Assert
			Assert.NotEqual(firstUpdatedAt, note.UpdatedAt);
		}
		#endregion
	}
}