using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Parameters;
using Horus.Domain.Tooling.Manifests.Parameters.Bindings;
using Horus.Domain.Tooling.Manifests.Parameters.Bindings.Rules;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters.Builders;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Parameters
{
	public class ParameterBindingTests
	{
		#region Invalid Cases
		[Fact]
		public void ParameterBinding_ShouldThrowBusinessRuleValidationException_WhenEntityIsEmpty()
		{
			// Arrange
			string emptyEntity = "";
			string jsonPath = "$.valid.path";
			var rule = new StringCannotBeEmptyOrNull(emptyEntity, "ParameterBinding.Entity");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterBinding.Create(emptyEntity, jsonPath));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ParameterBinding_ShouldThrowBusinessRuleValidationException_WhenEntityIsNull()
		{
			// Arrange
			string? nullEntity = null;
			string jsonPath = "$.valid.path";
			var rule = new StringCannotBeEmptyOrNull(nullEntity!, "ParameterBinding.Entity");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterBinding.Create(nullEntity!, jsonPath));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ParameterBinding_ShouldThrowBusinessRuleValidationException_WhenJsonPathIsEmpty()
		{
			// Arrange
			string entity = "ScanTarget";
			string jsonPath = "";
			var rule = new StringCannotBeEmptyOrNull(jsonPath, "ParameterBinding.JsonPath");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterBinding.Create(entity, jsonPath));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ParameterBinding_ShouldThrowBusinessRuleValidationException_WhenJsonPathIsNull()
		{
			// Arrange
			string entity = "ScanTarget";
			string? nullJsonPath = null;
			var rule = new StringCannotBeEmptyOrNull(nullJsonPath!, "ParameterBinding.JsonPath");

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterBinding.Create(entity, nullJsonPath!));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void ParameterBinding_ShouldThrowBusinessRuleValidationException_WhenJsonPathDoesntStartWithDollarSign()
		{
			// Arrange
			string entity = "ScanTarget";
			string jsonPath = "invalid.path";
			var rule = new BindingJsonPathMustStartWithDollar(jsonPath);

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => ParameterBinding.Create(entity, jsonPath));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Constructor
		[Fact]
		public void ParameterBinding_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange
			string entity = "ScanTarget";
			string jsonPath = "$.name";
			// Act
			var binding = ParameterBinding.Create(entity, jsonPath);

			// Assert
			Assert.NotNull(binding);
			Assert.Equal(entity, binding.Entity);
			Assert.Equal(jsonPath, binding.JsonPath);
		}
		#endregion
	}
}