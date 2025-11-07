using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.SharedKernel.FilePaths;

namespace Horus.Domain.Notes.Findings
{
	public sealed class Finding : Entity
	{
		// Attributes
		public FindingId Id { get; private init; } = default!;
		public EntityName Title { get; private set; } = default!;
		public FilePath FilePath { get; private set; } = default!;
		public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

		// Relations
		public NetworkHost? NetworkHost { get; private set; }
		public NetworkHostId? NetworkHostId { get; private set; }
		public ScanTarget? ScanTarget { get; private set; }
		public ScanTargetId? ScanTargetId { get; private set; }

		// For EF Only
		[Obsolete("EF Needed", true)]
		private Finding() { }

		private Finding(FindingId id, EntityName title, FilePath filePath, NetworkHostId? networkHostId = null, ScanTargetId? scanTargetId = null)
		{
			Id = id;
			Title = title;
			FilePath = filePath;
			ScanTargetId = scanTargetId;
			NetworkHostId = networkHostId;
		}

		public static Finding ForNetworkHost(string title, NetworkHostId networkHostId, IFindingPathHandler findingPathHandler)
		{
			FindingId id = new();
			EntityName findingTitle = EntityName.FromString(title);
			FilePath path = findingPathHandler.CreateForNetworkHost(id, networkHostId);

			return new(
				id,
				findingTitle,
				path,
				networkHostId
			);
		}
		public static Finding ForScanTarget(string title, ScanTargetId scanTargetId, IFindingPathHandler findingPathHandler)
		{
			FindingId id = new();
			EntityName findingTitle = EntityName.FromString(title);
			FilePath path = findingPathHandler.CreateForScanTarget(id, scanTargetId);

			return new(
				id,
				findingTitle,
				path,
				scanTargetId: scanTargetId
			);
		}

		public void UpdateTitle(string title)
		{
			EntityName newTitle = EntityName.FromString(title);
			Title = newTitle;
		}

		public void MarkUpdated()
		{
			UpdatedAt = DateTime.UtcNow;
		}
	}
}