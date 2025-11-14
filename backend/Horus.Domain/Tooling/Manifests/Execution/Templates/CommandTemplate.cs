using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.SharedRules;

namespace Horus.Domain.Tooling.Manifests.Execution.Templates
{
	public sealed class CommandTemplate : ValueObject
	{
		public string Value { get; }

		private CommandTemplate(string value)
		{
			Value = value;
		}

		public static CommandTemplate Create(string value)
		{
			CheckRule(new StringCannotBeEmptyOrNull(value, nameof(CommandTemplate)));

			return new CommandTemplate(value.Trim());
		}
	}
}