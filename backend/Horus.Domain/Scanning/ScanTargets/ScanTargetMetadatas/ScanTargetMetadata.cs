using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets.ScanTargetMetadatas
{
	public sealed class ScanTargetMetadata : ValueObject
	{
		public string? Description { get; }

		private ScanTargetMetadata(string? description)
		{
			Description = description?.Trim();
		}

		public static ScanTargetMetadata Create(string description)
		{
			CheckRule(new Rules.ScanTargetDescriptionCannotBeNullOrEmpty(description));
			CheckRule(new Rules.ScanTargetDescriptionLengthMustBeInRange(description));

			return new ScanTargetMetadata(description);
		}
	}
}