using Horus.Domain.Findings.Vulnerabilities.Evidences;
using Horus.Domain.Findings.Vulnerabilities.Evidences.Rules;
using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Findings.Vulnerabilities
{
	public class EvidenceTests
	{
		#region Constructor Exception Tests
		[Fact]
		public void Evidence_ShouldThrowBusinessRuleValidationException_WhenValueIsEmpty()
		{
			// Arrange
			string invalidEvidence = string.Empty;
			var rule = new EvidenceCannotBeNullOrEmpty(invalidEvidence);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() =>
			{
				var evidence = Evidence.Create(invalidEvidence);
			});
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Evidence_ShouldThrowBusinessRuleValidationException_WhenValueIsNull()
		{
			// Arrange
			string? invalidEvidence = null;
			var rule = new EvidenceCannotBeNullOrEmpty(invalidEvidence!);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() =>
			{
				var evidence = Evidence.Create(invalidEvidence!);
			});
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Evidence_ShouldThrowBusinessRuleValidationException_WhenValueIsNotValidJson()
		{
			// Arrange
			string invalidEvidence = "This is not a JSON string.";
			var rule = new EvidenceMustBeAValidJson(invalidEvidence);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() =>
			{
				var evidence = Evidence.Create(invalidEvidence);
			});
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Constructor Success Tests
		[Fact]
		public void Evidence_ShouldCreate_WhenValueIsValidJson()
		{
			// Arrange
			string validEvidence = "{\"key\":\"value\"}";

			// Act
			var evidence = Evidence.Create(validEvidence);

			// Assert
			Assert.NotNull(evidence);
			Assert.Equal(validEvidence, evidence.Value);
		}

		[Fact]
		public void Evidence_ShouldTrimValue_WhenValueHasLeadingOrTrailingWhitespace()
		{
			// Arrange
			string validEvidenceWithWhitespace = "   {\"key\":\"value\"}   ";
			string expectedTrimmedValue = "{\"key\":\"value\"}";

			// Act
			var evidence = Evidence.Create(validEvidenceWithWhitespace);

			// Assert
			Assert.NotNull(evidence);
			Assert.Equal(expectedTrimmedValue, evidence.Value);
		}
		#endregion

		#region ToObject Tests
		[Fact]
		public void Evidence_ToObject_ShouldReturnDeserializedObject_WhenValueIsValidJson()
		{
			// Arrange
			string validEvidence = "{\"Name\":\"Test\",\"Value\":123}";
			var evidence = Evidence.Create(validEvidence);

			// Act
			var deserializedObject = evidence.ToObject<TestObject>();

			// Assert
			Assert.NotNull(deserializedObject);
			Assert.Equal("Test", deserializedObject.Name);
			Assert.Equal(123, deserializedObject.Value);
		}

		[Fact]
		public void Evidence_ToObject_ShouldReturnValidObject_WhenObjectTypeIsIncompatible()
		{
			// Arrange
			string validEvidence = "{\"InvalidName\":\"Test\",\"InvalidValue\":123}";
			var evidence = Evidence.Create(validEvidence);

			// Act
			var testObject = evidence.ToObject<TestObject>();

			// Act & Assert
			Assert.IsType<TestObject>(testObject);
			Assert.Null(testObject.Name);
			Assert.Equal(0, testObject.Value);
		}

		#endregion
	}

	public class TestObject
	{
		public string Name { get; set; } = default!;
		public int Value { get; set; }
	}
}