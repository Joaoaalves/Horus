using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Domain.Tooling.Manifests.Execution.Templates;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Execution
{
	public class CommandTemplateTests
	{
		#region Invalid Cases
		[Fact]
		public void CommandTemplate_ShouldThrowBusinessRuleValidationException_WhenTemplateIsEmpty()
		{
			// Arrange
			string emptyTemplate = "";
			var rule = new StringCannotBeEmptyOrNull(emptyTemplate, nameof(CommandTemplate));

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(() => CommandTemplate.Create(emptyTemplate));

			// Assert
			Assert.Equal(rule.Message, exc.Message);
		}
		#endregion

		#region Valid Cases
		[Fact]
		public void CommandTemplate_ShouldCreateSuccessfully_WhenValidTemplateIsProvided()
		{
			// Arrange
			string validTemplate = "echo 'Hello, World!'";

			// Act
			var commandTemplate = CommandTemplate.Create(validTemplate);

			// Assert
			Assert.NotNull(commandTemplate);
			Assert.Equal(validTemplate, commandTemplate.Value);
		}
		#endregion
	}
}