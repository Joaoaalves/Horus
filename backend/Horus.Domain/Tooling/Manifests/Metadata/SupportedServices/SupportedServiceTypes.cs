using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Metadata.SupportedServices
{
	public sealed class SupportedServiceTypes : ValueObject
	{
		public HashSet<string> ServiceTypes { get; } = [];

		private SupportedServiceTypes(HashSet<string> serviceTypes)
		{
			ServiceTypes = serviceTypes;
		}

		public static SupportedServiceTypes Create(HashSet<string> serviceTypes)
		{
			CheckRule(new Rules.SupportedServiceTypesCannotBeEmpty(serviceTypes));

			foreach (var serv in serviceTypes)
				CheckRule(new StringCannotBeEmptyOrNull(serv, "SupportedServiceTypes.ServiceTypes[]"));
			return new SupportedServiceTypes(serviceTypes);
		}

		public static SupportedServiceTypes FromList(IEnumerable<string> serviceTypes)
		{
			return Create([.. serviceTypes]);
		}

		public List<string> ToList()
		{
			return [.. ServiceTypes];
		}

		/// <summary>
		/// Checks if the given service type is supported.
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns>
		/// True if the service type is supported; otherwise, false.
		/// </returns>
		public bool Support(string serviceType)
		{
			return ServiceTypes.Contains(serviceType);
		}
	}
}