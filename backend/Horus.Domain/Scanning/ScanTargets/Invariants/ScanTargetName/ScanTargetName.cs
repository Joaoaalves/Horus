using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetName
{
	public sealed class ScanTargetName : ValueObject
	{
		public string Value { get; }

		private ScanTargetName(string value)
		{
			Value = value;
		}

		public static ScanTargetName FromString(string value)
		{
			CheckRule(new Rules.ScanTargetNameCanNotBeNull(value));

			CheckRule(new Rules.ScanTargetNameLengthMustBeInRange(value));

			return new ScanTargetName(value);
		}

		public override string ToString() => Value;
	}
}