using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parser
{
	public class RegexPatternTests
	{
		#region Invalid Cases
		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string emptyName = "";
			string regex = "regex";
			string severity = "critical";
			var rule = new StringCannotBeEmptyOrNull(emptyName, "RegexPattern.Name");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(emptyName, regex, severity));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? nullName = null;
			string regex = "regex";
			string severity = "critical";
			var rule = new StringCannotBeEmptyOrNull(nullName!, "RegexPattern.Name");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(nullName!, regex, severity));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenRegexIsEmpty()
		{
			// Arrange
			string name = "name";
			string emptyRegex = "";
			string severity = "critical";
			var rule = new StringCannotBeEmptyOrNull(emptyRegex, "RegexPattern.Regex");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(name, emptyRegex, severity));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenRegexIsNull()
		{
			// Arrange
			string name = "name";
			string? nullRegex = null;
			string severity = "critical";
			var rule = new StringCannotBeEmptyOrNull(nullRegex!, "RegexPattern.Regex");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(name!, nullRegex!, severity));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenSeverityIsEmpty()
		{
			// Arrange
			string name = "name";
			string regex = "regex";
			string emptySeverity = "";
			var rule = new StringCannotBeEmptyOrNull(emptySeverity, "RegexPattern.Severity");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(name, regex, emptySeverity));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void RegexPattern_ShouldThrowBusinessRuleValidationException_WhenSeverityIsNull()
		{
			// Arrange
			string name = "name";
			string regex = "regex";
			string? nullSeverity = null;
			var rule = new StringCannotBeEmptyOrNull(nullSeverity!, "RegexPattern.Severity");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => RegexPattern.Create(name, regex, nullSeverity!));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Constructor
		[Fact]
		public void RegexPattern_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			string name = "name";
			string regex = "regex";
			string severity = "critical";

			// Act
			var regexPattern = RegexPattern.Create(name, regex, severity);

			// Assert
			Assert.NotNull(regexPattern);
			Assert.Equal(name, regexPattern.Name);
			Assert.Equal(regex, regexPattern.Regex);
			Assert.Equal(severity, regexPattern.Severity);
		}
		#endregion
	}
}