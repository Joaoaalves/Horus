using Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortNumbers;
using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkPorts
{
	public class NetworkPortNumberTests
	{
		[Fact]
		public void NetworkPortNumber_ShouldThrowBusinessRuleValidationException_WhenValueIsGreatherThanBoundary()
		{
			// Arrange
			uint portNumber = 65536;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPortNumber.Create(portNumber));
		}

		[Fact]
		public void NetworkPortNumber_ShouldThrowBusinessRuleValidationException_WhenValueIsLowerThanBoundary()
		{
			// Arrange
			uint portNumber = 0;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPortNumber.Create(portNumber));
		}

		[Fact]
		public void NetworkPortNumber_ShouldBeCreated_WhenValueIsOnBoundary()
		{
			// Arrange
			uint portNumber = 22;

			// Act
			var networkPort = NetworkPortNumber.Create(portNumber);

			// Assert
			Assert.NotNull(networkPort);
			Assert.Equal(portNumber, networkPort.Value);
		}
	}
}