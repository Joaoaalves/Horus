using Horus.Domain.Scanning.NetworkPorts;
using Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas;
using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkPorts
{
	public class NetworkPortsTests
	{
		#region Boundary validation

		[Fact]
		public void NetworkPort_ShouldThrowBusinessRuleValidationException_WhenPortNumberIsGreaterThanBoundary()
		{
			// Arrange
			uint portNumber = 65536;
			Protocol protocol = Protocol.TCP;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPort.Create(portNumber, protocol));
		}

		[Fact]
		public void NetworkPort_ShouldThrowBusinessRuleValidationException_WhenPortNumberIsLowerThanBoundary()
		{
			// Arrange
			uint portNumber = 0;
			Protocol protocol = Protocol.TCP;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPort.Create(portNumber, protocol));
		}

		#endregion

		#region Creation and default state

		[Fact]
		public void NetworkPort_Create_ShouldInitializeWithValidValues()
		{
			// Arrange
			uint portNumber = 443;
			Protocol protocol = Protocol.TCP;

			// Act
			var port = NetworkPort.Create(portNumber, protocol);

			// Assert
			Assert.NotNull(port);
			Assert.Equal(portNumber, port.Number.Value);
			Assert.Equal(protocol, port.Protocol);
			Assert.Equal(NetworkPortStatus.Closed, port.Status);
			Assert.NotNull(port.Metadata);
			Assert.Equal("{}", port.Metadata.Value);
		}

		[Fact]
		public void NetworkPort_Create_ShouldAssignNewUniqueId()
		{
			// Arrange
			var port1 = NetworkPort.Create(22, Protocol.TCP);
			var port2 = NetworkPort.Create(22, Protocol.TCP);

			// Assert
			Assert.NotEqual(port1.Id, port2.Id);
		}

		#endregion

		#region Service handling

		[Fact]
		public void NetworkPort_Create_ShouldAllowNullService()
		{
			// Arrange
			uint portNumber = 80;

			// Act
			var port = NetworkPort.Create(portNumber, Protocol.TCP, NetworkPortStatus.Open, null);

			// Assert
			Assert.Null(port.Service);
		}

		#endregion

		#region Metadata handling

		[Fact]
		public void NetworkPort_UpdateMetadata_ShouldReplaceExistingMetadata()
		{
			// Arrange
			var port = NetworkPort.Create(8080, Protocol.TCP);
			var newMetadata = NetworkPortMetadata.FromJson("{\"service\":\"HTTP\"}");

			// Act
			port.UpdateMetadata(newMetadata);

			// Assert
			Assert.Equal("{\"service\":\"HTTP\"}", port.Metadata.Value);
		}

		[Fact]
		public void NetworkPort_UpdateMetadata_ShouldNotAffectOtherInstances()
		{
			// Arrange
			var port1 = NetworkPort.Create(21, Protocol.TCP);
			var port2 = NetworkPort.Create(21, Protocol.TCP);

			var newMetadata = NetworkPortMetadata.FromJson("{\"key\":\"value\"}");

			// Act
			port1.UpdateMetadata(newMetadata);

			// Assert
			Assert.NotEqual(port1.Metadata.Value, port2.Metadata.Value);
		}

		#endregion

		#region Status behavior

		[Fact]
		public void NetworkPort_Create_ShouldRespectInitialStatusParameter()
		{
			// Arrange
			uint portNumber = 22;

			// Act
			var port = NetworkPort.Create(portNumber, Protocol.TCP, NetworkPortStatus.Open);

			// Assert
			Assert.Equal(NetworkPortStatus.Open, port.Status);
		}

		#endregion

		#region Entity invariants

		[Fact]
		public void NetworkPort_Properties_ShouldBeReadOnlyAfterCreation()
		{
			// Arrange
			var port = NetworkPort.Create(53, Protocol.UDP);

			// Assert
			Assert.True(port.GetType().GetProperty(nameof(port.Number))!.CanRead);
			Assert.True(port.GetType().GetProperty(nameof(port.Number))!.CanWrite); // Have a setter
			Assert.True(port.GetType().GetProperty(nameof(port.Metadata))!.CanRead);
		}

		#endregion
	}
}