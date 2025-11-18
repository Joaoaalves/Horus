using Horus.Domain.Executions.JobStatuses;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.NetworkPorts;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.Jsons;
using Horus.Domain.Tooling.Manifests;
using Horus.Domain.Tooling.Manifests.Identity;

namespace Horus.Domain.Executions.ToolExecutions
{
	public sealed class ToolExecutionJob : Entity, IAggregateRoot
	{
		// Parameters
		public ToolExecutionJobId Id { get; } = default!;
		public Json Parameters { get; private set; }
		public ExecutionStatus Status { get; private set; } = ExecutionStatus.Pending;
		public Json? ResultMetadata { get; private set; }
		public DateTime QueuedAt { get; } = DateTime.UtcNow;
		public DateTime StartedAt { get; private set; }
		public DateTime FinishedAt { get; private set; }

		// Relations
		public ToolManifestId ManifestId { get; } = default!;
		public ToolManifest Manifest { get; } = default!;

		public NetworkHost? NetworkHost { get; }
		public NetworkHostId? NetworkHostId { get; }

		public NetworkPort? NetworkPort { get; }
		public NetworkPortId? NetworkPortId { get; }

		private ToolExecutionJob(ToolExecutionJobId id, Json parameters, ToolManifestId manifestId, NetworkHostId? networkHostId = null, NetworkPortId? networkPortId = null)
		{
			Id = id;
			Parameters = parameters;
			ManifestId = manifestId;
			NetworkHostId = networkHostId;
			NetworkPortId = networkPortId;
		}

		public static ToolExecutionJob ForNetworkHost(NetworkHostId networkHostId, string parameters, ToolManifestId manifestId)
		{

			return new ToolExecutionJob(
				new ToolExecutionJobId(),
				Json.FromString(parameters),
				manifestId,
				networkHostId: networkHostId
			);
		}

		public static ToolExecutionJob ForNetworkPort(NetworkPortId networkPortId, string parameters, ToolManifestId manifestId)
		{
			return new ToolExecutionJob(
				new ToolExecutionJobId(),
				Json.FromString(parameters),
				manifestId,
				networkPortId: networkPortId
			);
		}

		public void Finish(bool success, Json result)
		{
			ResultMetadata = result;
			Status = success ? ExecutionStatus.Succeded : ExecutionStatus.Failed;
			FinishedAt = DateTime.UtcNow;
		}

		public void Start()
		{
			Status = ExecutionStatus.Running;
			StartedAt = DateTime.UtcNow;
		}
	}
}