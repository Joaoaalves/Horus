using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parameters.Types;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Boolean;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Enum;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.FilePath;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Number;
using Horus.Domain.Tooling.Manifests.Parameters.Types.Rules.Url;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters.Builders;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters
{
	public class ParameterTypeTests
	{
		#region Invalid Construction Cases
		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenEmptyTypeIsReceived()
		{
			// Arrange
			string emptyType = string.Empty;
			var rule = new StringCannotBeEmptyOrNull(emptyType, "ToolParameters.ParameterType");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterType.Create(emptyType));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenNullTypeIsReceived()
		{
			// Arrange
			string? emptyType = null;
			var rule = new StringCannotBeEmptyOrNull(emptyType!, "ToolParameters.ParameterType");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterType.Create(emptyType!));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenEmptyEnumOptionsAreReceived()
		{
			// Arrange
			string emptyEnum = "enum:";
			var rule = new EnumOptionsCannotBeEmpty([]);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterType.Create(emptyEnum));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void Create_ShouldThrowBusinessRuleValidationException_WhenNotSupportedTypeIsReceived()
		{
			// Arrange
			string notSupportedType = "int";
			var rule = new ParameterTypeCannotAcceptNotSupportedType(["number"], notSupportedType);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterType.Create(notSupportedType));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Construction Cases
		[Fact]
		public void Create_ShouldReturnValidParameterType_WhenSupportedTypeIsReceived()
		{
			// Arrange
			string supportedType = "number";

			// Act
			var paramType = ParameterType.Create(supportedType);

			// Assert
			Assert.NotNull(paramType);
			Assert.Equal(supportedType, paramType.Value);
		}

		[Fact]
		public void Create_ShouldReturnValidParameterType_WhenEnumWithValidOptionsIsReceived()
		{
			// Arrange
			List<string> enumOptions = ["enum1", "enum2"];
			string enumType = $"enum:{string.Join(",", enumOptions)}";

			// Act
			var paramType = ParameterType.Create(enumType);

			// Assert
			Assert.NotNull(paramType);
			Assert.Equal("enum", paramType.Value);

			// Assert all possible enum options
			foreach (var option in enumOptions)
			{
				var exception = Record.Exception(() => paramType.ValidateValue(option));
				Assert.Null(exception);
			}
		}
		#endregion

		#region ValidateValue
		[Fact]
		public void ValidateValue_ShouldThrowBusinessRuleValidationException_WhenTypeIsNumberAndValueIsNotValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForNumber();
			var value = "string";
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => paramType.ValidateValue(value));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateValue_ShouldThrowBusinessRuleValidationException_WhenTypeIsBooleanAndValueIsNotValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForBoolean();
			var value = "string";
			var rule = new ParameterTypeBooleanMustReceiveAValidValue(value);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => paramType.ValidateValue(value));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		[Fact]
		public void ValidateValue_ShouldThrowBusinessRuleValidationException_WhenTypeIsUrlAndValueIsNotValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForUrl();
			var value = "htttps://invalid!url.com";
			var rule = new ParameterTypeUrlMustReceiveAValidValue(value);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => paramType.ValidateValue(value));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateValue_ShouldThrowBusinessRuleValidationException_WhenTypeIsPathAndValueIsNotValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForPath();
			var value = "<invalid|path>";
			var rule = new ParameterTypePathMustReceiveAValidValue(value);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => paramType.ValidateValue(value));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateValue_ShouldThrowBusinessRuleValidationException_WhenTypeIsEnumAndValueIsNotValid()
		{
			// Arrange
			var value = "option2";
			List<string> values = ["option1"];
			var paramType = ParameterTypeBuilder.ForEnum(values);
			var rule = new EnumValueMustBeValid(values, value);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => paramType.ValidateValue(value));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsString()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForString();
			var value = "string";

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}

		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsBooleanAndValueIsValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForBoolean();
			var value = "true";
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}
		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsUrlAndValueIsValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForUrl();
			var value = "http://valid-url.com";
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}

		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsPathAndValueIsValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForPath();
			var value = "/valid/path";
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}
		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsEnumAndValueIsValid()
		{
			// Arrange
			List<string> options = ["option1", "option2"];
			var paramType = ParameterTypeBuilder.ForEnum(options);
			var value = options[0];
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}

		[Fact]
		public void ValidateValue_ShouldReturn_WhenTypeIsNumberAndValueIsValid()
		{
			// Arrange
			var paramType = ParameterTypeBuilder.ForNumber();
			var value = "1";
			var rule = new ParameterTypeNumberMustReceiveAValidNumber(value);

			// Act
			var exc = Record.Exception(() => paramType.ValidateValue(value));

			// Assert
			Assert.Null(exc);
		}
		#endregion
	}
}