using Horus.Domain.SeedWork;
using Horus.Domain.Tooling.Manifests.Metadata.SupportedServices;
using Horus.Domain.Tooling.Manifests.Metadata.SupportedServices.Rules;

namespace Horus.Tests.Unit.Domain.Tooling.Manifests.Metadata
{
	public class SupportedServiceTypesTests
	{
		#region Invalid Creation Tests
		[Fact]
		public void SupportedServices_ShouldThrowBusinessRuleValidationException_WhenServiceTypesAreEmpty()
		{
			// Arrange
			var serviceTypes = new HashSet<string>();
			var rule = new SupportedServiceTypesCannotBeEmpty(serviceTypes);

			// Act & Assert
			Assert.Throws<BusinessRuleValidationException>(() => SupportedServiceTypes.Create(serviceTypes));
		}
		#endregion

		#region Valid Creation Tests
		[Fact]
		public void SupportedServices_ShouldCreateSuccessfully_WhenServiceTypesHashSetAreProvided()
		{
			// Arrange
			var serviceTypes = new HashSet<string> { "ServiceA", "ServiceB" };

			// Act
			var supportedServices = SupportedServiceTypes.Create(serviceTypes);

			// Assert
			Assert.NotNull(supportedServices);
			Assert.Equal(2, supportedServices.ServiceTypes.Count);
			Assert.Contains("ServiceA", supportedServices.ServiceTypes);
			Assert.Contains("ServiceB", supportedServices.ServiceTypes);
		}

		[Fact]
		public void SupportedServices_ShouldCreateSuccessfully_WhenServiceTypesListAreProvided()
		{
			// Arrange
			var serviceTypesList = new List<string> { "ServiceA", "ServiceB" };

			// Act
			var supportedServices = SupportedServiceTypes.FromList(serviceTypesList);

			// Assert
			Assert.NotNull(supportedServices);
			Assert.Equal(2, supportedServices.ServiceTypes.Count);
			Assert.Contains("ServiceA", supportedServices.ServiceTypes);
			Assert.Contains("ServiceB", supportedServices.ServiceTypes);
		}
		#endregion

		#region Support Method Tests
		[Fact]
		public void SupportedServices_Support_ShouldReturnTrue_WhenServiceTypeIsSupported()
		{
			// Arrange
			var serviceTypes = new HashSet<string> { "ServiceA", "ServiceB" };
			var supportedServices = SupportedServiceTypes.Create(serviceTypes);

			// Act
			var isSupported = supportedServices.Support("ServiceA");

			// Assert
			Assert.True(isSupported);
		}

		[Fact]
		public void SupportedServices_Support_ShouldReturnFalse_WhenServiceTypeIsNotSupported()
		{
			// Arrange
			var serviceTypes = new HashSet<string> { "ServiceA", "ServiceB" };
			var supportedServices = SupportedServiceTypes.Create(serviceTypes);

			// Act
			var isSupported = supportedServices.Support("ServiceC");

			// Assert
			Assert.False(isSupported);
		}

		[Fact]
		public void SupportedServices_Support_ShouldReturnFalse_WhenNullOrEmptyServiceTypeIsProvided()
		{
			// Arrange
			var serviceTypes = new HashSet<string> { "ServiceA", "ServiceB" };
			var supportedServices = SupportedServiceTypes.Create(serviceTypes);

			// Act
			var isSupportedNull = supportedServices.Support(null!);
			var isSupportedEmpty = supportedServices.Support(string.Empty);

			// Assert
			Assert.False(isSupportedNull);
			Assert.False(isSupportedEmpty);
		}
		#endregion
	}
}