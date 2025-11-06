using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetName.Rules
{
	public sealed class ScanTargetNameCanNotBeNull(string value) : IBusinessRule
	{
		private readonly string _targetName = value;
		public string Message => "Target name cannot be null";
		public bool IsBroken() => String.IsNullOrEmpty(_targetName);

	}
}