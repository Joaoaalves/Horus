using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.EntityDescriptions.Rules
{
	public sealed class EntityDescriptionLengthMustBeInRange(string value) : IBusinessRule
	{
		private readonly string _description = value;
		private readonly uint _minLength = 5;
		private readonly uint _maxLength = 300;
		public string Message => $"Description cannot be smaller than {_minLength} and greather than {_maxLength}";
		public bool IsBroken() => _description.Length < _minLength || _description.Length > _maxLength;
	}
}