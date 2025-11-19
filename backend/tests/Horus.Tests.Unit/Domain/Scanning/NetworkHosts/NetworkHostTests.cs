using Horus.Domain.Findings.Notes;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;
using Moq;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkHosts
{
	public sealed class NetworkHostTests
	{
		#region Invalid Construct
		[Fact]
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? name = null;
			string address = "192.168.0.1";

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkHost.Create(name!, address));
		}

		[Fact]
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string name = string.Empty;
			string address = "192.168.0.1";

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkHost.Create(name!, address));
		}

		[Fact]
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenAddressIsNull()
		{
			// Arrange
			string name = "Network Host";
			string? address = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkHost.Create(name, address!));
		}

		[Fact]
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenAddressIsInvalid()
		{
			// Arrange
			string name = "Network Host";
			string address = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkHost.Create(name, address));
		}

		#endregion

		#region  Valid Construct
		[Fact]
		public void NetworkHost_ShouldCreate_WhenNameAndAddressAreProvided()
		{
			// Arrange
			string name = "Network Host";
			string address = "192.168.0.1";

			// Act
			var networkHost = NetworkHost.Create(name, address);

			// Assert
			Assert.NotNull(networkHost);
			Assert.Equal(name, networkHost.Name.Value);
			Assert.Equal(address, networkHost.Address.Value);
		}
		#endregion

		#region Rename
		[Fact]
		public void Rename_ShouldRename_WhenNewNameIsValid()
		{
			// Arrange
			string name = "Network Host";
			string address = "192.168.0.1";

			// Assert & Act
			var networkHost = NetworkHost.Create(name, address);
			Assert.NotNull(networkHost);
			Assert.Equal(name, networkHost.Name.Value);

			var newName = "New Name";
			networkHost.Rename(newName);
			Assert.Equal(newName, networkHost.Name.Value);
		}

		[Fact]
		public void Rename_ShouldThrowBusinessRuleValidationException_WhenNewNameIsInvalid()
		{
			// Arrange
			string name = "Network Host";
			string address = "192.168.0.1";

			// Assert & Act
			var networkHost = NetworkHost.Create(name, address);
			Assert.NotNull(networkHost);
			Assert.Equal(name, networkHost.Name.Value);

			var newName = string.Empty;
			Assert.Throws<BusinessRuleValidationException>(() => networkHost.Rename(newName));
		}
		#endregion

		#region  UpdateAddress
		[Fact]
		public void UpdateAddress_ShouldUpdateAddress_WhenNewAddressIsValid()
		{
			// Arrange
			string name = "Network Host";
			string address = "192.168.0.1";

			// Assert & Act
			var networkHost = NetworkHost.Create(name, address);
			Assert.NotNull(networkHost);
			Assert.Equal(address, networkHost.Address.Value);

			var newAddress = "192.168.0.2";
			networkHost.UpdateAddress(newAddress);
			Assert.Equal(newAddress, networkHost.Address.Value);
		}

		[Fact]
		public void UpdateAddress_ShouldThrowBusinessRuleValidationException_WhenNewAddressIsValid()
		{
			// Arrange
			string name = "Network Host";
			string address = "192.168.0.1";

			// Assert & Act
			var networkHost = NetworkHost.Create(name, address);
			Assert.NotNull(networkHost);
			Assert.Equal(address, networkHost.Address.Value);

			var newAddress = "http://exa|mple.com";
			Assert.Throws<BusinessRuleValidationException>(() => networkHost.UpdateAddress(newAddress));
		}
		#endregion

		#region IAnnotable - AddNote
		[Fact]
		public void AddNote_ShouldReturn_WhenNullNoteIsProvided()
		{
			// Arrange
			Note? note = null;
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			// Act
			var exc = Record.Exception(() => networkHost.AddNote(note!));

			// Assert
			Assert.Null(exc);
			Assert.Empty(networkHost.Notes);
		}

		[Fact]
		public void AddNote_ShouldAddNote_WhenValidNoteIsProvided()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);

			// Act
			var exc = Record.Exception(() => networkHost.AddNote(note));

			// Assert
			Assert.Null(exc);
			Assert.Single(networkHost.Notes);
		}

		[Fact]
		public void AddNote_ShouldAddAnotherNote_WhenNotesAreNoteEmpty()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);
			Note note2 = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);

			// Act
			networkHost.AddNote(note);
			Assert.Single(networkHost.Notes);
			networkHost.AddNote(note2);

			// Assert
			Assert.Equal(2, networkHost.Notes.Count);
			Assert.Contains(note2, networkHost.Notes);
		}

		[Fact]
		public void AddNote_ShouldntAddAnotherNote_WhenNotesAreEqualEmpty()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);

			// Act
			networkHost.AddNote(note);
			Assert.Single(networkHost.Notes);
			networkHost.AddNote(note);

			// Assert
			Assert.Single(networkHost.Notes);
		}
		#endregion

		#region IAnnotable - RemoveNote
		[Fact]
		public void RemoveNote_ShouldRemoveNote_WhenNoteIsPresent()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);
			networkHost.AddNote(note);

			// Act
			Assert.Single(networkHost.Notes);
			var exc = Record.Exception(() => networkHost.RemoveNote(note));

			// Assert
			Assert.Null(exc);
			Assert.Empty(networkHost.Notes);
		}

		[Fact]
		public void RemoveNote_ShouldReturn_WhenNoteIsNotPresent()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);
			Note note2 = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);
			networkHost.AddNote(note);

			// Act
			Assert.Single(networkHost.Notes);
			var exc = Record.Exception(() => networkHost.RemoveNote(note2));

			// Assert
			Assert.Null(exc);
			Assert.Single(networkHost.Notes);
		}

		[Fact]
		public void RemoveNote_ShouldReturn_WhenNoteIsNull()
		{
			string name = StringBuilder.Build(10);
			string address = "192.168.0.1";
			var networkHost = NetworkHost.Create(name, address);

			var noteTile = StringBuilder.Build(10);
			var fakeNotePathHandler = new Mock<INotePathHandler>();
			Note note = Note.ForNetworkHost(noteTile, networkHost.Id, fakeNotePathHandler.Object);
			Note? note2 = null;
			networkHost.AddNote(note);

			// Act
			Assert.Single(networkHost.Notes);
			var exc = Record.Exception(() => networkHost.RemoveNote(note2!));

			// Assert
			Assert.Null(exc);
			Assert.Single(networkHost.Notes);
		}
		#endregion
	}
}