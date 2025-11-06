using Horus.Domain.SeedWork.Commands;
using Horus.Domain.SeedWork;
using DomainUnit = Horus.Domain.SeedWork.Commands.Unit;

namespace Horus.Tests.Unit.Application.ConfigurationTests.Fakes
{
	public class FakeCommand : IRequest<DomainUnit>
	{
		public string Name { get; set; } = string.Empty;
	}

	public class FakeCommandString : ICommand<string>
	{
		public Guid Id => Guid.NewGuid();
	}
}