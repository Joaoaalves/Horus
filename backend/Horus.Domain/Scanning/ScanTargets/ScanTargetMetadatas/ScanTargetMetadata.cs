using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityDescriptions;

namespace Horus.Domain.Scanning.ScanTargets.ScanTargetMetadatas
{
	public sealed class ScanTargetMetadata : ValueObject
	{
		public EntityDescription? Description { get; }

		private ScanTargetMetadata(EntityDescription description)
		{
			Description = description;
		}

		public static ScanTargetMetadata Create(string description)
		{
			var scanTargetDesc = EntityDescription.FromString(description);

			return new ScanTargetMetadata(scanTargetDesc);
		}
	}
}