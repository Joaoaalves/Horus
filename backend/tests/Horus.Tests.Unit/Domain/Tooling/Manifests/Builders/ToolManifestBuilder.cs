using Horus.Domain.Tooling.Manifests;
using Horus.Domain.Tooling.Manifests.Execution;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;
using Horus.Domain.Tooling.Manifests.Identity;
using Horus.Domain.Tooling.Manifests.Metadata;
using Horus.Domain.Tooling.Manifests.Parameters;
using Horus.Domain.Tooling.Manifests.Parsers;
using Horus.Domain.Tooling.Manifests.Parsers.Payload.Regex;
using Horus.Tests.Unit.Builders;
using Horus.Tests.Unit.Domain.Tooling.Manifests.Execution.Fakes;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Builders
{
	public static class ToolManifestBuilder
	{
		public static ToolManifest Build(
			ManifestIdentity? identity = null,
			ManifestMetadata? metadata = null,
			ManifestExecution? execution = null,
			IEnumerable<ToolParameter>? parameters = null,
			ToolResultParser? parser = null
		)
		{
			// ID
			if (identity is null)
			{
				string name = StringBuilder.Build(5);
				string description = StringBuilder.Build(5);
				identity = ManifestIdentity.Create(name, description);
			}

			// Metadata
			if (metadata is null)
			{
				int version = 1;
				IEnumerable<string> categories = [StringBuilder.Build(5)];
				var source = ManifestSource.Repo;
				IEnumerable<string> supportedServices = [StringBuilder.Build(5)];
				metadata = ManifestMetadata.Create(version, categories, source, supportedServices);
			}

			// Execution
			if (execution is null)
			{
				var fakeContainerImageVerifier = new FakeContainerImageVerifier(true);
				var image = ContainerImage.Create("valid:latest", fakeContainerImageVerifier);
				var commandTemplate = "echo success";
				execution = ManifestExecution.Create(image, commandTemplate);
			}


			// Parameters
			if (parameters is null)
			{
				var paramName = StringBuilder.Build(5);
				var paramType = "number";
				bool required = true;
				parameters = [ToolParameter.Create(paramName, paramType, required)];
			}

			// Parser
			if (parser is null)
			{
				string parserType = "regex";
				IEnumerable<RegexPattern> patterns = [];
				var payload = new RegexParserPayload(patterns);
				var outputPath = "/valid/path";
				parser = ToolResultParser.Create(parserType, payload, outputPath);
			}

			return ToolManifest.Create(identity, metadata, execution, parameters, parser);
		}
	}
}