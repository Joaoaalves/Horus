using Horus.Domain.Tooling.Manifests;
using Horus.Domain.Tooling.Manifests.Execution;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;
using Horus.Domain.Tooling.Manifests.Identity;
using Horus.Domain.Tooling.Manifests.Metadata;
using Horus.Domain.Tooling.Manifests.Metadata.Categories;
using Horus.Domain.Tooling.Manifests.Parameters;
using Horus.Domain.Tooling.Manifests.Parsers;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Execution.Fakes;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests
{
	public class ToolManifestTests
	{
		#region Valid Constructor Cases
		[Fact]
		public void ToolManifest_ShouldCreateSuccessfully_WhenValidParametersAreProvided()
		{
			// Arrange

			// IDENTITY
			string name = "name";
			string description = "description";
			var identity = ManifestIdentity.Create(name, description);

			// Metadata
			int version = 1;
			IEnumerable<string> categories = ["web"];
			var source = ManifestSource.Repo;
			IEnumerable<string> supportedServices = ["http"];
			var metadata = ManifestMetadata.Create(version, categories, source, supportedServices);

			// Execution
			var fakeContainerImageVerifier = new FakeContainerImageVerifier(true);
			var image = ContainerImage.Create("valid:latest", fakeContainerImageVerifier);
			var commandTemplate = "echo success";
			var execution = ManifestExecution.Create(image, commandTemplate);

			// Parameters
			var paramName = "paramName";
			var paramType = "number";
			bool required = true;
			IEnumerable<ToolParameter> parameters = [ToolParameter.Create(paramName, paramType, required)];

			// Parser
			string parserType = "regex";
			IEnumerable<RegexPattern> patterns = [];
			var payload = new RegexParserPayload(patterns);
			var outputPath = "/valid/path";
			var parser = ToolResultParser.Create(parserType, payload, outputPath);

			// Act
			var toolManifest = ToolManifest.Create(identity, metadata, execution, parameters, parser);

			// Assert
			Assert.Equal(name, toolManifest.Identity.Name.Value);
			Assert.NotNull(toolManifest.Identity.Description);
			Assert.Equal(description, toolManifest.Identity.Description.Value);

			Assert.Equal(version, toolManifest.Metadata.Version.Value);
			foreach (var cat in categories)
			{
				var category = ToolCategory.FromString(cat);
				Assert.Contains(category, toolManifest.Metadata.Categories);
			}
			Assert.Equal(source, toolManifest.Metadata.Source);
			Assert.Equal(supportedServices.Count(), toolManifest.Metadata.SupportedServices.ServiceTypes.Count);

			Assert.Equal(image, toolManifest.Execution.Image);
			Assert.Equal(commandTemplate, toolManifest.Execution.CommandTemplate.Value);

			// Parameter
			var firstParameter = toolManifest.Parameters.First();
			Assert.Equal(paramName, firstParameter.Name);
			Assert.Equal(paramType, firstParameter.Type.Value);
			Assert.Equal(required, firstParameter.Required);

			Assert.Equal(parserType, toolManifest.ResultParser.Type.Value);
			Assert.Equal(outputPath, toolManifest.ResultParser.OutputPath.Value);
		}
		#endregion
	}
}