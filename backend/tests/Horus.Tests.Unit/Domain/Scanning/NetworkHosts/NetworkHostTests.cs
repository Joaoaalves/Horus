using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkHosts
{
	public sealed class NetworkHostTests
	{
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

		[Fact]
		public void NetworkHost_ShouldRename_WhenNewNameIsValid()
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
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenNewNameIsInvalid()
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

		[Fact]
		public void NetworkHost_ShouldUpdateAddress_WhenNewAddressIsValid()
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
		public void NetworkHost_ShouldThrowBusinessRuleValidationException_WhenNewAddressIsValid()
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
	}
}