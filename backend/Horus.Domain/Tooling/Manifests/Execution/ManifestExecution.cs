using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Execution.ContainerImages;
using Horus.Domain.Tooling.Manifests.Execution.Templates;

namespace Horus.Domain.Tooling.Manifests.Execution
{
	public sealed class ManifestExecution : ValueObject
	{
		public ContainerImage Image { get; } = default!;
		public CommandTemplate CommandTemplate { get; } = default!;

		private ManifestExecution(
			ContainerImage image,
			CommandTemplate commandTemplate
		)
		{
			Image = image;
			CommandTemplate = commandTemplate;
		}

		public static ManifestExecution Create(
			ContainerImage image,
			string commandTemplate
		)
		{
			return new ManifestExecution(
				image,
				CommandTemplate.Create(commandTemplate)
			);
		}
	}
}