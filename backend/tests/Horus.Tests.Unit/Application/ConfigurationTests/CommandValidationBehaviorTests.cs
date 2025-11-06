using FluentValidation;
using Horus.Application.Configuration.Exceptions;
using Horus.Application.Configuration.Validation;
using Horus.Tests.Unit.Application.ConfigurationTests.Fakes;

using DomainUnit = Horus.Domain.SeedWork.Commands.Unit;

namespace Horus.Tests.Unit.Application.ConfigurationTests
{
	public class CommandValidationBehaviorTests
	{
		[Fact]
		public async Task Handle_ShouldCallNext_WhenNoValidators()
		{
			var behavior = new CommandValidationBehavior<FakeCommand, DomainUnit>(
				new List<IValidator<FakeCommand>>()
			);

			var command = new FakeCommand { Name = "test" };

			var nextCalled = false;
			Task<DomainUnit> Next()
			{
				nextCalled = true;
				return Task.FromResult(DomainUnit.Value);
			}

			var result = await behavior.Handle(command, Next, CancellationToken.None);

			Assert.True(nextCalled);
			Assert.Equal(DomainUnit.Value, result);
		}

		[Fact]
		public async Task Handle_ShouldCallNext_WhenValidationPasses()
		{
			var validators = new List<IValidator<FakeCommand>> { new FakeCommandValidator() };
			var behavior = new CommandValidationBehavior<FakeCommand, DomainUnit>(validators);
			var command = new FakeCommand { Name = "Valid" };

			var result = await behavior.Handle(command, () => Task.FromResult(DomainUnit.Value), CancellationToken.None);

			Assert.Equal(DomainUnit.Value, result);
		}

		[Fact]
		public async Task Handle_ShouldThrowInvalidCommandException_WhenValidationFails()
		{
			var validators = new List<IValidator<FakeCommand>> { new FakeCommandValidator() };
			var behavior = new CommandValidationBehavior<FakeCommand, DomainUnit>(validators);
			var command = new FakeCommand { Name = "" }; // invalid

			var ex = await Assert.ThrowsAsync<InvalidCommandException>(() =>
				behavior.Handle(command, () => Task.FromResult(DomainUnit.Value), CancellationToken.None)
			);

			Assert.Equal("Name is required", ex.Message);
			Assert.Equal("Name is required", ex.Details);
		}
	}
}