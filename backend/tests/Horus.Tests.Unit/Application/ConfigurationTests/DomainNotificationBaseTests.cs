using Horus.Application.Configuration.DomainEvents;
using Horus.Tests.Unit.Domain.Fakes;

namespace Horus.Tests.Unit.Application.ConfigurationTests
{
	public class DomainNotificationBaseTests
	{
		[Fact]
		public void Constructor_ShouldSetDomainEvent()
		{
			// Arrange
			var domainEvent = new FakeDomainEvent();

			// Act
			var notification = new DomainNotificationBase<FakeDomainEvent>(domainEvent);

			// Assert
			Assert.Equal(domainEvent, notification.DomainEvent);
		}

		[Fact]
		public void Constructor_ShouldGenerateId()
		{
			// Arrange
			var domainEvent = new FakeDomainEvent();

			// Act
			var notification = new DomainNotificationBase<FakeDomainEvent>(domainEvent);

			// Assert
			Assert.NotEqual(Guid.Empty, notification.Id);
		}
	}
}