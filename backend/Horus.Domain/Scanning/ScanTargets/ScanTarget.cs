using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.EntityNames;
using Horus.Domain.Scanning.ScanTargets.ScanTargetMetadatas;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Findings.Notes;

namespace Horus.Domain.Scanning.ScanTargets
{
	public sealed class ScanTarget : Entity, IAnnotable, IAggregateRoot
	{
		// Backing Fields 
		private readonly List<NetworkHost> _hosts = [];
		private readonly List<Note> _notes = [];

		// Attributes
		public ScanTargetId Id { get; private set; } = default!;
		public EntityName Name { get; private set; } = default!;
		public ScanTargetMetadata? Metadata { get; private set; }

		// Relations
		public IReadOnlyCollection<NetworkHost> Hosts => _hosts.AsReadOnly();
		public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();

		[Obsolete("For EF Only", true)]
		private ScanTarget() { }

		private ScanTarget(ScanTargetId id, EntityName name, ScanTargetMetadata? metadata = null)
		{
			Id = id;
			Name = name;
			Metadata = metadata;
		}

		public static ScanTarget Create(string name, string? description = null)
		{
			var targetId = new ScanTargetId();
			var targetName = EntityName.FromString(name);
			var targetMetadata = string.IsNullOrWhiteSpace(description)
				? null
				: ScanTargetMetadata.Create(description);

			return new ScanTarget(targetId, targetName, targetMetadata);
		}

		public void Rename(string name)
		{
			var targetName = EntityName.FromString(name);
			Name = targetName;
		}

		public void UpdateMetadata(string description)
		{
			var metadata = ScanTargetMetadata.Create(description);
			Metadata = metadata;
		}

		public void AddNetworkHost(NetworkHost host)
		{
			if (host is not null)
				_hosts.Add(host);
		}

		public void RemoveNetworkHost(NetworkHost host)
		{
			_hosts.Remove(host);
		}

		public void AddNote(Note note)
		{
			if (note is not null && !_notes.Contains(note)) _notes.Add(note);
		}

		public void RemoveNote(Note note)
		{
			_notes.Remove(note);
		}
	}
}