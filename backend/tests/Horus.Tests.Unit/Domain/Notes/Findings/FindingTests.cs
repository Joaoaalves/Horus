using Horus.Domain.Notes.Findings;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames.Rules;
using Horus.Tests.Unit.Builders;
using Horus.Tests.Unit.Domain.Fakes;
using Moq;

namespace Horus.Tests.Unit.Domain.Notes.Findings
{
	public class FindingTests
	{

		#region Creation with invalid state

		[Fact]
		public static void Finding_ShouldThrowBusinessRuleValidationException_WhenTitleIsEmpty()
		{
			// Arrange
			string title = string.Empty;
			ScanTargetId id = new();
			var rule = new EntityNameCanNotBeNull(title);
			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act & Arrange
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Finding.ForScanTarget(title, id, fakeFindingPathHandler.Object));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public static void Finding_ShouldThrowBusinessRuleValidationException_WhenTitleIsNull()
		{
			// Arrange
			string? title = null;
			ScanTargetId id = new();
			var rule = new EntityNameCanNotBeNull(title!);

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act & Arrange
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Finding.ForScanTarget(title!, id, fakeFindingPathHandler.Object));
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region ForNetworkHost

		[Fact]
		public static void Finding_ForNetworkHost_ShouldReturnAFindingWithSameNetworkHostId()
		{
			// Arrange
			string title = "Valid Title!";
			NetworkHostId hostId = new();

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act
			var finding = Finding.ForNetworkHost(title, hostId, fakeFindingPathHandler.Object);

			// Assert
			Assert.NotNull(finding);
			Assert.Equal(hostId, finding.NetworkHostId);
			Assert.Null(finding.ScanTargetId);
		}

		#endregion

		#region ForScanTarget

		[Fact]
		public static void Finding_ForScanTarget_ShouldReturnAFindingWithSameScanTargetId()
		{
			// Arrange
			string title = "Valid Title!";
			ScanTargetId scanTargetId = new();

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act
			var finding = Finding.ForScanTarget(title, scanTargetId, fakeFindingPathHandler.Object);

			// Assert
			Assert.NotNull(finding);
			Assert.Equal(scanTargetId, finding.ScanTargetId);
			Assert.Null(finding.NetworkHostId);
		}

		#endregion

		#region UpdateTitle
		[Fact]
		public static void Finding_UpdateTitle_ShouldUpdateFindingTitleCorrectly()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act
			var finding = Finding.ForScanTarget(title, scanTargetId, fakeFindingPathHandler.Object);

			// Act & Assert
			Assert.Equal(title, finding.Title.Value);

			string newTitle = "New Title!";
			finding.UpdateTitle(newTitle);
			Assert.Equal(newTitle, finding.Title.Value);
		}

		[Fact]
		public static void Finding_UpdateTitle_ShouldThrowBusinessRuleValidationException_WhenTitleIsInvalid()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act
			var finding = Finding.ForScanTarget(title, scanTargetId, fakeFindingPathHandler.Object);

			// Act & Assert
			Assert.Equal(title, finding.Title.Value);

			string shortTitle = StringBuilder.Build(1);
			Assert.Throws<BusinessRuleValidationException>(() =>
				finding.UpdateTitle(shortTitle)
			);

			string longTitle = StringBuilder.Build(200);
			Assert.Throws<BusinessRuleValidationException>(() =>
				finding.UpdateTitle(longTitle)
			);

			string emptyTitle = string.Empty;
			Assert.Throws<BusinessRuleValidationException>(() =>
				finding.UpdateTitle(emptyTitle)
			);
		}

		#endregion

		#region MarkUpdated
		[Fact]
		public void Finding_MarkUpdated_ShouldCorrectlySetUpdatedAtValue()
		{
			// Arrange
			string title = "First Title!";
			ScanTargetId scanTargetId = new();

			var fakeFindingPathHandler = new Mock<IFindingPathHandler>();

			// Act
			var finding = Finding.ForScanTarget(title, scanTargetId, fakeFindingPathHandler.Object);
			var firstUpdatedAt = finding.UpdatedAt;
			finding.MarkUpdated();

			// Assert
			Assert.NotEqual(firstUpdatedAt, finding.UpdatedAt);
		}
		#endregion
	}
}