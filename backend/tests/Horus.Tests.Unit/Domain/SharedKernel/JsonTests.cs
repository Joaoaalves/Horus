using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.Jsons;
using Horus.Domain.SharedKernel.Jsons.Rules;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Tests.Unit.Domain.SharedKernel
{
	public class JsonTests
	{
		#region Invalid Cases

		[Fact]
		public void FromString_ShouldThrow_WhenEmptyStringIsProvided()
		{
			// Arrange
			string input = string.Empty;
			var rule = new StringCannotBeEmptyOrNull(input, nameof(Json));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Json.FromString(input));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FromString_ShouldThrow_WhenNullStringIsProvided()
		{
			// Arrange
			string? input = null;
			var rule = new StringCannotBeEmptyOrNull(input!, nameof(Json));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Json.FromString(input!));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FromString_ShouldThrow_WhenInvalidJsonIsProvided()
		{
			// Arrange
			string invalidJson = "{ invalid json }";
			var rule = new JsonMustBeValid(invalidJson);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => Json.FromString(invalidJson));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		#endregion

		#region Valid Cases

		[Fact]
		public void FromString_ShouldReturnValidJson_WhenValidJsonIsProvided()
		{
			// Arrange
			string validJson = """{"key":"value","num":1}""";

			// Act
			var json = Json.FromString(validJson);

			// Assert
			Assert.NotNull(json);
			Assert.Equal(validJson, json.Value);
		}

		[Fact]
		public void FromString_ShouldAcceptEmptyObject()
		{
			// Arrange
			string validJson = "{}";

			// Act
			var json = Json.FromString(validJson);

			// Assert
			Assert.NotNull(json);
			Assert.Equal(validJson, json.Value);
		}

		#endregion
	}
}