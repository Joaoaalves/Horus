using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.SharedKernel.FilePaths;

namespace Horus.Domain.Findings.Notes
{
	public sealed class Note : Entity
	{
		// Attributes
		public NoteId Id { get; private init; } = default!;
		public EntityName Title { get; private set; } = default!;
		public FilePath FilePath { get; private set; } = default!;
		public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

		// Relations
		public NetworkHost? NetworkHost { get; private set; }
		public NetworkHostId? NetworkHostId { get; private set; }

		public ScanTarget? ScanTarget { get; private set; }
		public ScanTargetId? ScanTargetId { get; private set; }

		[Obsolete("EF Only", true)]
		private Note() { }

		private Note(NoteId id, EntityName title, FilePath filePath, NetworkHostId? hostId, ScanTargetId? targetId)
		{
			Id = id;
			Title = title;
			FilePath = filePath;

			NetworkHostId = hostId;
			ScanTargetId = targetId;
		}

		public static Note ForNetworkHost(string title, NetworkHostId hostId, INotePathHandler pathHandler)
		{
			var id = new NoteId();
			return new(
				id,
				EntityName.FromString(title),
				pathHandler.CreateForNetworkHost(id, hostId),
				hostId,
				null
			);
		}

		public static Note ForScanTarget(string title, ScanTargetId targetId, INotePathHandler pathHandler)
		{
			var id = new NoteId();
			return new(
				id,
				EntityName.FromString(title),
				pathHandler.CreateForScanTarget(id, targetId),
				null,
				targetId
			);
		}

		public void UpdateTitle(string title)
		{
			Title = EntityName.FromString(title);
		}

		public void MarkUpdated()
		{
			UpdatedAt = DateTime.UtcNow;
		}
	}
}