using Horus.Domain.SeedWork;

namespace Horus.Domain.Tooling.Manifests.Metadata.SupportedServices.Rules
{
	public sealed class SupportedServiceTypesCannotBeEmpty(IEnumerable<string> services) : IBusinessRule
	{
		private readonly IEnumerable<string> _services = services;
		public string Message => "Supported service types cannot be empty.";

		public bool IsBroken() => !_services.Any();
	}
}