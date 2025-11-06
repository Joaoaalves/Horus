using FluentValidation;

namespace Horus.Tests.Unit.Application.ConfigurationTests.Fakes
{
	public class FakeCommandValidator : AbstractValidator<FakeCommand>
	{
		public FakeCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
		}
	}
}