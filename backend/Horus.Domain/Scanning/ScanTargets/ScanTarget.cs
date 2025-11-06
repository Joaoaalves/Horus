using Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetMetadata;
using Horus.Domain.Scanning.ScanTargets.Invariants.ScanTargetName;
using Horus.Domain.SeedWork;

namespace Horus.Domain.Scanning.ScanTargets
{
	public sealed class ScanTarget : Entity, IAggregateRoot
	{
		// Backing Fields 

		// Attributes
		public ScanTargetId Id { get; private set; } = default!;
		public ScanTargetName Name { get; private set; } = default!;
		public ScanTargetMetadata? Metadata { get; private set; }

		[Obsolete("For EF Only", true)]
		private ScanTarget() { }

		private ScanTarget(ScanTargetId id, ScanTargetName name, ScanTargetMetadata? metadata = null)
		{
			Id = id;
			Name = name;
			Metadata = metadata;
		}

		public static ScanTarget Create(string name, string? description = null)
		{
			var targetId = new ScanTargetId();
			var targetName = ScanTargetName.FromString(name);
			var targetMetadata = string.IsNullOrWhiteSpace(description)
				? null
				: ScanTargetMetadata.Create(description);

			return new ScanTarget(targetId, targetName, targetMetadata);
		}

		public void Rename(string name)
		{
			var targetName = ScanTargetName.FromString(name);
			Name = targetName;
		}

		public void UpdateMetadata(string description)
		{
			var metadata = ScanTargetMetadata.Create(description);
			Metadata = metadata;
		}
	}
}