using Horus.Domain.SeedWork;

namespace Horus.Application.Configuration.DomainEvents
{
	public interface IDomainEventNotification<TEventType> : IDomainEventNotification
	{
		TEventType DomainEvent { get; }
	}

	public interface IDomainEventNotification : INotification
	{
		Guid Id { get; }
	}
}