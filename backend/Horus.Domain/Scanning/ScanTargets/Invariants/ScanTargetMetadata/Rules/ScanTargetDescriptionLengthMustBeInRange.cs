using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetMetadata.Rules
{
	public sealed class ScanTargetDescriptionLengthMustBeInRange(string value) : IBusinessRule
	{
		private readonly string _description = value;
		private readonly uint _minLength = 5;
		private readonly uint _maxLength = 300;
		public string Message => $"Scan Target description cannot be smaller than {_minLength} and greather than {_maxLength}";
		public bool IsBroken() => _description.Length < _minLength || _description.Length > _maxLength;
	}
}