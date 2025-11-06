using Horus.Domain.SeedWork;

namespace Horus.Tests.Unit.Domain.Fakes
{
	public class FakeTypedId(Guid value) : TypedIdValueBase(value)
	{
	}
}