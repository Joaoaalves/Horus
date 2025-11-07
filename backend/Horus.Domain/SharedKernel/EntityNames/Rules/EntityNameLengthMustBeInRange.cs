using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.EntityNames.Rules
{
	public sealed class EntityNameLengthMustBeInRange(string value) : IBusinessRule
	{
		private readonly string _targetName = value;
		private readonly uint _minLength = 3;
		private readonly uint _maxLength = 100;
		public string Message => $"Target name cannot be smaller than {_minLength} and greather than {_maxLength}";
		public bool IsBroken() => _targetName.Length < _minLength || _targetName.Length > _maxLength;

	}
}