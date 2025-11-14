using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Execution.ContainerImages
{
	public sealed class ContainerImage : ValueObject
	{
		public string Value { get; }

		private ContainerImage(string value)
		{
			Value = value;
		}

		public static ContainerImage Create(string value, IContainerImageVerifier verifier)
		{
			CheckRule(new StringCannotBeEmptyOrNull(value, nameof(ContainerImage)));
			CheckRule(new Rules.ContainerImageMustBeAValidImage(value, verifier));

			return new ContainerImage(value);
		}

		public string ToStringRepresentation()
		{
			return Value;
		}
	}
}