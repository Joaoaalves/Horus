using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets.ScanTargetMetadatas.Rules
{
	public sealed class ScanTargetDescriptionCannotBeNullOrEmpty(string value) : IBusinessRule
	{
		private readonly string _description = value;
		public string Message => "Scan Target description cannot be null or empty";

		public bool IsBroken() => String.IsNullOrEmpty(_description);
	}
}