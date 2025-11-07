using System.Text.Json;
using System.Text.Json.Nodes;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.NetworkPorts.Invariants.NetworkPortMetadatas
{
	public class NetworkPortMetadata : ValueObject
	{
		public string Value { get; } = "{}";

		private static readonly JsonSerializerOptions jsonOptions = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			PropertyNameCaseInsensitive = false
		};

		private NetworkPortMetadata(string value)
		{
			Value = string.IsNullOrWhiteSpace(value) ? "{}" : value;
		}

		public static NetworkPortMetadata Empty() => new("{}");

		public static NetworkPortMetadata FromObject(object data)
		{
			CheckRule(new Rules.NetworkPortMetadataShouldBeSerializable(data));

			var json = JsonSerializer.Serialize(data);
			return new NetworkPortMetadata(json);
		}

		public static NetworkPortMetadata FromJson(string json)
		{
			CheckRule(new Rules.NetworkPortMetadataJsonShouldNotBeEmpty(json));
			CheckRule(new Rules.NetworkPortMetadataShouldBeAValidJson(json));

			return new NetworkPortMetadata(json);
		}

		public T? ToObject<T>()
		{
			return JsonSerializer.Deserialize<T>(Value, jsonOptions);
		}

		/// <summary>
		/// Adds or updates a key with a value. Returns a new NetworkPortMetadata instance (immutability preserved).
		/// </summary>
		public NetworkPortMetadata AddOrUpdate(string key, object? value)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty.", nameof(key));

			var node = JsonNode.Parse(Value) as JsonObject ?? [];

			if (value is not null)
			{
				var jsonNode = JsonNode.Parse(JsonSerializer.Serialize(value, jsonOptions));
				node[key] = jsonNode;
			}
			else
			{
				node[key] = null;
			}

			return new NetworkPortMetadata(node.ToJsonString());
		}

		/// <summary>
		/// Removes a key (if it exists). Returns a new NetworkPortMetadata instance.
		/// </summary>
		public NetworkPortMetadata Remove(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty.", nameof(key));

			var node = JsonNode.Parse(Value) as JsonObject ?? [];
			node.Remove(key);

			return new NetworkPortMetadata(node.ToJsonString());
		}

		/// <summary>
		/// Gets a value by key, deserialized into type T. Returns default(T) if not found or null.
		/// </summary>
		public T? Get<T>(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty.", nameof(key));

			if (JsonNode.Parse(Value) is not JsonObject node || !node.TryGetPropertyValue(key, out var jsonNode) || jsonNode is null)
				return default;

			return jsonNode.Deserialize<T>();
		}

		/// <summary>
		/// Checks if a given key exists in the metadata.
		/// </summary>
		public bool ContainsKey(string key)
		{
			var node = JsonNode.Parse(Value) as JsonObject;
			return node is not null && node.ContainsKey(key);
		}

		protected IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}