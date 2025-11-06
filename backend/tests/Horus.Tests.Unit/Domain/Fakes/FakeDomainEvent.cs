using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Fakes
{
	public class FakeDomainEvent : IDomainEvent
	{
		public DateTime OccurredOn => new();
	}
}