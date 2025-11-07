using Horus.Domain.Scanning.NetworkPorts;
using Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas;
using Horus.Domain.SeedWork;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.Scanning.NetworkPorts
{
	public sealed class NetworkPortMetadataTests
	{
		#region Existing Rule Tests

		public static void NetworkPortMetadata_ShouldThrowBusinessRuleValidationException_WhenEmptyJsonIsProvided()
		{
			// Arrange
			string metadata = string.Empty;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPortMetadata.FromJson(metadata));
		}

		public static void NetworkPortMetadata_ShouldThrowBusinessRuleValidationException_WhenNullStringIsProvided()
		{
			// Arrange
			string? metadata = null;

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => NetworkPortMetadata.FromJson(metadata!));
		}

		public static void NetworkPortMetadata_ShouldThrowBusinessRuleValidationException_WhenInvalidJsonIsProvided()
		{
			// Arrange
			List<string> invalidMetadatas = JsonBuilder.BuildInvalidJsonList(10);

			// Act & Assert
			foreach (var metadata in invalidMetadatas)
				Assert.Throws<BusinessRuleValidationException>(() => NetworkPortMetadata.FromJson(metadata));
		}

		#endregion

		#region Factory Methods

		public static void NetworkPortMetadata_Empty_ShouldReturnEmptyJson()
		{
			// Act
			var metadata = NetworkPortMetadata.Empty();

			// Assert
			Assert.Equal("{}", metadata.Value);
		}

		public static void NetworkPortMetadata_FromObject_ShouldSerializeObjectToJson()
		{
			// Arrange
			var data = new { Name = "HTTP", Port = 80 };

			// Act
			var metadata = NetworkPortMetadata.FromObject(data);

			// Assert
			Assert.Contains("Name", metadata.Value);
			Assert.Contains("Port", metadata.Value);
			Assert.Contains("80", metadata.Value);
		}

		public static void NetworkPortMetadata_FromJson_ShouldCreateInstanceWithSameValue()
		{
			// Arrange
			var json = JsonBuilder.BuildValidJson();

			// Act
			var metadata = NetworkPortMetadata.FromJson(json);

			// Assert
			Assert.Equal(json, metadata.Value);
		}

		#endregion

		#region Conversion

		public static void NetworkPortMetadata_ToObject_ShouldDeserializeJsonToObject()
		{
			// Arrange
			var json = "{\"name\":\"SSH\",\"port\":22}";
			var metadata = NetworkPortMetadata.FromJson(json);

			// Act
			var result = metadata.ToObject<Dictionary<string, object>>();

			// Assert
			Assert.NotNull(result);
			Assert.Equal("SSH", result["name"].ToString());
			Assert.Equal(22, Convert.ToInt32(result["port"]));
		}

		#endregion

		#region AddOrUpdate

		public static void NetworkPortMetadata_AddOrUpdate_ShouldAddNewKey()
		{
			// Arrange
			var metadata = NetworkPortMetadata.Empty();

			// Act
			var updated = metadata.AddOrUpdate("protocol", "TCP");

			// Assert
			Assert.Contains("protocol", updated.Value);
			Assert.Contains("TCP", updated.Value);
		}

		public static void NetworkPortMetadata_AddOrUpdate_ShouldUpdateExistingKey()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"protocol\":\"UDP\"}");

			// Act
			var updated = metadata.AddOrUpdate("protocol", "TCP");

			// Assert
			Assert.DoesNotContain("UDP", updated.Value);
			Assert.Contains("TCP", updated.Value);
		}

		public static void NetworkPortMetadata_AddOrUpdate_ShouldHandleNullValue()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"protocol\":\"UDP\"}");

			// Act
			var updated = metadata.AddOrUpdate("protocol", null);

			// Assert
			Assert.Contains("\"protocol\":null", updated.Value);
		}

		#endregion

		#region Remove

		public static void NetworkPortMetadata_Remove_ShouldRemoveExistingKey()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"protocol\":\"TCP\",\"port\":80}");

			// Act
			var updated = metadata.Remove("protocol");

			// Assert
			Assert.DoesNotContain("protocol", updated.Value);
			Assert.Contains("port", updated.Value);
		}

		#endregion

		#region Get

		public static void NetworkPortMetadata_Get_ShouldReturnExistingValue()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"port\":443}");

			// Act
			var value = metadata.Get<int>("port");

			// Assert
			Assert.Equal(443, value);
		}

		public static void NetworkPortMetadata_Get_ShouldReturnDefaultForMissingKey()
		{
			// Arrange
			var metadata = NetworkPortMetadata.Empty();

			// Act
			var value = metadata.Get<string>("nonexistent");

			// Assert
			Assert.Null(value);
		}

		#endregion

		#region ContainsKey

		public static void NetworkPortMetadata_ContainsKey_ShouldReturnTrueIfKeyExists()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"service\":\"HTTP\"}");

			// Act
			var result = metadata.ContainsKey("service");

			// Assert
			Assert.True(result);
		}

		public static void NetworkPortMetadata_ContainsKey_ShouldReturnFalseIfKeyDoesNotExist()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"service\":\"HTTP\"}");

			// Act
			var result = metadata.ContainsKey("port");

			// Assert
			Assert.False(result);
		}

		#endregion

		#region Immutability

		public static void NetworkPortMetadata_AddOrUpdate_ShouldReturnNewInstance()
		{
			// Arrange
			var metadata = NetworkPortMetadata.Empty();

			// Act
			var updated = metadata.AddOrUpdate("key", "value");

			// Assert
			Assert.NotSame(metadata, updated);
		}

		public static void NetworkPortMetadata_Remove_ShouldReturnNewInstance()
		{
			// Arrange
			var metadata = NetworkPortMetadata.FromJson("{\"key\":\"value\"}");

			// Act
			var updated = metadata.Remove("key");

			// Assert
			Assert.NotSame(metadata, updated);
		}

		#endregion
	}
}