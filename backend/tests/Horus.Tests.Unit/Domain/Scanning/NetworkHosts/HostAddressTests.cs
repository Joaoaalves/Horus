using Horus.Domain.Scanning.NetworkHosts.HostAddresses;
using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkHosts
{
	public class HostAddressTests
	{
		[Fact]
		public void HostAddress_ShouldThrowBusinessRuleValidationException_WhenAddressIsNull()
		{
			// Arrange
			string? address = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => HostAddress.Create(address!));
		}

		[Fact]
		public void HostAddress_ShouldThrowBusinessRuleValidationException_WhenAddressIsEmpty()
		{
			// Arrange
			string address = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => HostAddress.Create(address));
		}

		[Fact]
		public void HostAddress_ShouldThrowBusinessRuleValidationException_WhenAddressIsInvalid()
		{
			// Arrange
			string address = $"http://examp|le.com";

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => HostAddress.Create(address));
		}

		[Fact]
		public void HostAddress_ShouldThrowBusinessRuleValidationException_WhenIpIsInvalid()
		{
			// Arrange
			string address = "999999999";

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => HostAddress.Create(address));
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WhenUrlIsValid()
		{
			// Arrange
			string address = $"example.com";

			// Act
			var hostAddr = HostAddress.Create(address);

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WhenUrlAndProtocolIsValid()
		{
			// Arrange
			string protocol = "http://";
			string address = $"example.com";

			// Act
			var hostAddr = HostAddress.Create($"{protocol}{address}");

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
			Assert.DoesNotContain(hostAddr.Value, protocol);
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WhenIpIsValid()
		{
			// Arrange
			string address = $"192.168.0.1";

			// Act
			var hostAddr = HostAddress.Create($"{address}");

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WhenIpAndProtocolIsValid()
		{
			// Arrange
			string protocol = "http://";
			string address = $"192.168.0.1";

			// Act
			var hostAddr = HostAddress.Create($"{protocol}{address}");

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
			Assert.DoesNotContain(hostAddr.Value, protocol);
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WithIpAndProtocolTypo()
		{
			// Arrange
			string protocol = "htttp://";
			string address = $"192.168.0.1";

			// Act
			var hostAddr = HostAddress.Create($"{protocol}{address}");

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
			Assert.DoesNotContain(hostAddr.Value, protocol);
		}

		[Fact]
		public void HostAddress_ShouldBeCreated_WithUrlAndProtocolTypo()
		{
			// Arrange
			string protocol = "htttp://";
			string address = $"example.com";

			// Act
			var hostAddr = HostAddress.Create($"{protocol}{address}");

			// Assert
			Assert.NotNull(hostAddr);
			Assert.Equal(address, hostAddr.Value);
			Assert.DoesNotContain(hostAddr.Value, protocol);
		}
	}
}