using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.EntityNames.Rules
{
	public sealed class EntityNameCanNotBeNull(string value) : IBusinessRule
	{
		private readonly string _targetName = value;
		public string Message => "Target name cannot be null";
		public bool IsBroken() => String.IsNullOrEmpty(_targetName);

	}
}