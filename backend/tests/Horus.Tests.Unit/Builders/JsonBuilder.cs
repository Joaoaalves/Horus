using System.Text.Json;
using System.Text.Json.Nodes;

namespace Horus.Tests.Unit.Builders
{
	public static class JsonBuilder
	{
		private static readonly Random _rand = new();

		/// <summary>
		/// Generates a valid JSON string with a given number of random properties.
		/// </summary>
		/// <param name="propertyCount">Number of properties to include in the JSON object.</param>
		public static string BuildValidJson(int propertyCount = 3)
		{
			var jsonObject = new JsonObject();

			for (int i = 0; i < propertyCount; i++)
			{
				string key = $"key{i + 1}";
				object value = GenerateRandomValue();
				jsonObject[key] = JsonValue.Create(value);
			}

			return jsonObject.ToJsonString(new JsonSerializerOptions
			{
				WriteIndented = false
			});
		}

		/// <summary>
		/// Generates a list of valid JSON strings.
		/// </summary>
		/// <param name="count">Number of JSON objects to generate.</param>
		/// <param name="propertyCount">Number of properties per JSON object.</param>
		public static List<string> BuildValidJsonList(int count = 5, int propertyCount = 3)
		{
			var list = new List<string>();

			for (int i = 0; i < count; i++)
				list.Add(BuildValidJson(propertyCount));

			return list;
		}

		/// <summary>
		/// Generates an invalid JSON string for testing validation failures.
		/// </summary>
		public static string BuildInvalidJson()
		{
			// Some intentionally malformed JSON examples
			string[] invalidSamples =
			{
				"{unclosed",
				"{ 'key': 'value' ",
				"{ key: value }",
				"[1, 2, 3",
				"{\"key\": \"value\",,}",
				"{\"key\": null invalid}"
			};

			return invalidSamples[_rand.Next(invalidSamples.Length)];
		}

		/// <summary>
		/// Generates a list of invalid JSON strings.
		/// </summary>
		/// <param name="count">Number of invalid JSONs to generate.</param>
		public static List<string> BuildInvalidJsonList(int count = 5)
		{
			var list = new List<string>();

			for (int i = 0; i < count; i++)
				list.Add(BuildInvalidJson());

			return list;
		}

		/// <summary>
		/// Generates a random simple value (string, number, boolean, or null).
		/// </summary>
		private static object GenerateRandomValue()
		{
			int type = _rand.Next(0, 4);
			return type switch
			{
				0 => $"value{_rand.Next(100)}",
				1 => _rand.Next(0, 1000),
				2 => _rand.NextDouble() < 0.5,
				_ => null!
			};
		}
	}
}