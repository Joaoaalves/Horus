using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parameters;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters.Builders;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters
{
	public class ToolParameterTests
	{
		#region Invalid Cases
		[Fact]
		public void ToolParameter_ShouldThrowBusinessRuleValidationException_WhenNameIsEmpty()
		{
			// Arrange
			string emptyName = "";
			string description = "description";
			string defaultValue = "1";
			string type = "object";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;
			var rule = new StringCannotBeEmptyOrNull(emptyName, "ToolParameter.Name");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolParameter.Create(
					emptyName,
					type,
					required,
					defaultValue,
					parameterBinding,
					description)
			);

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ToolParameter_ShouldThrowBusinessRuleValidationException_WhenNameIsNull()
		{
			// Arrange
			string? nullName = null;
			string description = "description";
			string defaultValue = "1";
			string type = "object";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;
			var rule = new StringCannotBeEmptyOrNull(nullName!, "ToolParameter.Name");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolParameter.Create(
					nullName!,
					type,
					required,
					defaultValue,
					parameterBinding,
					description)
			);

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ToolParameter_ShouldThrowBusinessRuleValidationException_WhenTypeIsEmpty()
		{
			// Arrange
			string name = "Valid Name";
			string description = "description";
			string defaultValue = "1";
			string emptyType = "";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;
			var rule = new StringCannotBeEmptyOrNull(emptyType, "ToolParameter.Type");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolParameter.Create(
					name,
					emptyType,
					required,
					defaultValue,
					parameterBinding,
					description)
			);

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ToolParameter_ShouldThrowBusinessRuleValidationException_WhenTypeIsNull()
		{
			// Arrange
			string validName = "Valid Name";
			string description = "description";
			string defaultValue = "1";
			string? nullType = null;
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;
			var rule = new StringCannotBeEmptyOrNull(nullType!, "ToolParameter.Type");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolParameter.Create(
					validName,
					nullType!,
					required,
					defaultValue,
					parameterBinding,
					description)
			);

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Constructor Cases
		[Fact]
		public void ToolParameter_ShouldCreateSuccessfull_WhenValidParametersAreProvided()
		{
			// Arrange
			string name = "Valid Name";
			string description = "description";
			string defaultValue = "1";
			string type = "object";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;

			// Act
			var toolParameter = ToolParameter.Create(name, type, required, defaultValue, parameterBinding, description);

			// Assert
			Assert.NotNull(toolParameter);
			Assert.Equal(name, toolParameter.Name);
			Assert.Equal(type, toolParameter.Type);
			Assert.Equal(required, toolParameter.Required);
			Assert.Equal(defaultValue, toolParameter.DefaultValue);
			Assert.Equal(parameterBinding, toolParameter.Binding);
			Assert.Equal(description, toolParameter.Description);
		}

		[Fact]
		public void ToolParameter_ShouldCreateSuccessfull_WhenNoDescriptionIsProvided()
		{
			// Arrange
			string name = "Valid Name";
			string defaultValue = "1";
			string type = "object";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;

			// Act
			var toolParameter = ToolParameter.Create(name, type, required, defaultValue, parameterBinding);

			// Assert
			Assert.NotNull(toolParameter);
			Assert.Equal(name, toolParameter.Name);
			Assert.Equal(type, toolParameter.Type);
			Assert.Equal(required, toolParameter.Required);
			Assert.Equal(defaultValue, toolParameter.DefaultValue);
			Assert.Equal(parameterBinding, toolParameter.Binding);
			Assert.Null(toolParameter.Description);
		}

		[Fact]
		public void ToolParameter_ShouldCreateSuccessfull_WhenNoBindingIsProvided()
		{
			// Arrange
			string name = "Valid Name";
			string description = "description";
			string defaultValue = "1";
			string type = "object";
			bool required = true;

			// Act
			var toolParameter = ToolParameter.Create(name, type, required, defaultValue, description: description);

			// Assert
			Assert.NotNull(toolParameter);
			Assert.Equal(name, toolParameter.Name);
			Assert.Equal(type, toolParameter.Type);
			Assert.Equal(required, toolParameter.Required);
			Assert.Equal(defaultValue, toolParameter.DefaultValue);
			Assert.Null(toolParameter.Binding);
			Assert.Equal(description, toolParameter.Description);
		}

		[Fact]
		public void ToolParameter_ShouldCreateSuccessfull_WhenNoDefaultValueIsProvided()
		{
			// Arrange
			string name = "Valid Name";
			string description = "description";
			string type = "object";
			var parameterBinding = ParameterBindingBuilder.Build();
			bool required = true;

			// Act
			var toolParameter = ToolParameter.Create(name, type, required, binding: parameterBinding, description: description);

			// Assert
			Assert.NotNull(toolParameter);
			Assert.Equal(name, toolParameter.Name);
			Assert.Equal(type, toolParameter.Type);
			Assert.Equal(required, toolParameter.Required);
			Assert.Null(toolParameter.DefaultValue);
			Assert.Equal(parameterBinding, toolParameter.Binding);
			Assert.Equal(description, toolParameter.Description);
		}

		[Fact]
		public void ToolParameter_ShouldCreateSuccessfull_WhenNoDefaultValueBindingAndDescriptionIsProvided()
		{
			// Arrange
			string name = "Valid Name";
			string type = "object";
			bool required = true;

			// Act
			var toolParameter = ToolParameter.Create(name, type, required);

			// Assert
			Assert.NotNull(toolParameter);
			Assert.Equal(name, toolParameter.Name);
			Assert.Equal(type, toolParameter.Type);
			Assert.Equal(required, toolParameter.Required);
			Assert.Null(toolParameter.DefaultValue);
			Assert.Null(toolParameter.Binding);
			Assert.Null(toolParameter.Description);
		}
		#endregion
	}
}