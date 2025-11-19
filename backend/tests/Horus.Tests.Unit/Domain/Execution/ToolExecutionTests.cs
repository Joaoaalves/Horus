using Horus.Domain.Executions.JobStatuses;
using Horus.Domain.Executions.ToolExecutions;
using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.NetworkPorts;
using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.Jsons;
using Horus.Domain.Tooling.Manifests.Identity;

namespace Horus.Tests.Unit.Domain.Execution
{
	public class ToolExecutionJobTests
	{
		#region Helpers
		private static string ValidJson => """{"param": "value"}""";
		#endregion

		#region Construction ForNetworkHost

		[Fact]
		public void ForNetworkHost_ShouldCreateJobWithNetworkHostAndJsonParameters()
		{
			// Arrange
			var hostId = new NetworkHostId();
			var manifestId = new ToolManifestId();

			// Act
			var job = ToolExecutionJob.ForNetworkHost(hostId, ValidJson, manifestId);

			// Assert
			Assert.NotNull(job);
			Assert.Equal(hostId, job.NetworkHostId);
			Assert.Null(job.NetworkPortId);
			Assert.Equal(ValidJson, job.Parameters.Value);
			Assert.Equal(ExecutionStatus.Pending, job.Status);
			Assert.NotEqual(default, job.QueuedAt);
			Assert.Equal(manifestId, job.ManifestId);
		}

		[Fact]
		public void ForNetworkHost_ShouldThrow_WhenJsonIsInvalid()
		{
			// Arrange
			var hostId = new NetworkHostId();
			var invalidJson = "{ invalid }";
			var manifestId = new ToolManifestId();


			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolExecutionJob.ForNetworkHost(hostId, invalidJson, manifestId)
			);

			// Assert
			Assert.Equal("Invalid Json format.", exc.Message);
		}

		#endregion


		#region Construction ForNetworkPort

		[Fact]
		public void ForNetworkPort_ShouldCreateJobWithNetworkPortAndJsonParameters()
		{
			// Arrange
			var portId = new NetworkPortId();
			var manifestId = new ToolManifestId();

			// Act
			var job = ToolExecutionJob.ForNetworkPort(portId, ValidJson, manifestId);

			// Assert
			Assert.NotNull(job);
			Assert.Equal(portId, job.NetworkPortId);
			Assert.Null(job.NetworkHostId);
			Assert.Equal(ValidJson, job.Parameters.Value);
			Assert.Equal(ExecutionStatus.Pending, job.Status);
			Assert.NotEqual(default, job.QueuedAt);
			Assert.Equal(manifestId, job.ManifestId);
		}

		[Fact]
		public void ForNetworkPort_ShouldThrow_WhenJsonIsInvalid()
		{
			// Arrange
			var portId = new NetworkPortId();
			var invalidJson = "{ invalid }";
			var manifestId = new ToolManifestId();

			// Act
			var exc = Assert.Throws<BusinessRuleValidationException>(
				() => ToolExecutionJob.ForNetworkPort(portId, invalidJson, manifestId)
			);

			// Assert
			Assert.Equal("Invalid Json format.", exc.Message);
		}

		#endregion


		#region Start

		[Fact]
		public void Start_ShouldUpdateStatusToRunning_AndSetStartedAt()
		{
			// Arrange
			var manifestId = new ToolManifestId();
			var job = ToolExecutionJob.ForNetworkHost(new NetworkHostId(), ValidJson, manifestId);

			// Act
			job.Start();

			// Assert
			Assert.Equal(ExecutionStatus.Running, job.Status);
			Assert.NotEqual(default, job.StartedAt);
		}

		#endregion


		#region Finish

		[Fact]
		public void Finish_ShouldSetStatusSucceded_WhenSuccessIsTrue()
		{
			// Arrange
			var manifestId = new ToolManifestId();
			var job = ToolExecutionJob.ForNetworkHost(new NetworkHostId(), ValidJson, manifestId);
			job.Start();

			var result = Json.FromString("""{"success":true}""");

			// Act
			job.Finish(true, result);

			// Assert
			Assert.Equal(ExecutionStatus.Succeded, job.Status);
			Assert.Equal(result.Value, job.ResultMetadata!.Value);
			Assert.NotEqual(default, job.FinishedAt);
		}

		[Fact]
		public void Finish_ShouldSetStatusFailed_WhenSuccessIsFalse()
		{
			// Arrange
			var manifestId = new ToolManifestId();
			var job = ToolExecutionJob.ForNetworkHost(new NetworkHostId(), ValidJson, manifestId);
			job.Start();

			var result = Json.FromString("""{"ok":false}""");

			// Act
			job.Finish(false, result);

			// Assert
			Assert.Equal(ExecutionStatus.Failed, job.Status);
			Assert.Equal(result.Value, job.ResultMetadata!.Value);
			Assert.NotEqual(default, job.FinishedAt);
		}

		#endregion

		#region Requeue
		[Fact]
		public void Requeue_ShouldCreateACopyWithNewId_WithCorrectNetworkPort()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkPortId networkPortId = new();
			var job = ToolExecutionJob.ForNetworkPort(networkPortId, ValidJson, manifestId);

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.NotNull(requeued);
			Assert.NotEqual(job, requeued);
			Assert.Equal(networkPortId, requeued.NetworkPortId);
			Assert.True(job.QueuedAt < requeued.QueuedAt);
			Assert.Null(requeued.NetworkHostId);
		}

		[Fact]
		public void Requeue_ShouldCreateACopyWithNewId_WithCorrectNetworkHost()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkHostId networkHostId = new();
			var job = ToolExecutionJob.ForNetworkHost(networkHostId, ValidJson, manifestId);

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.NotNull(requeued);
			Assert.NotEqual(job, requeued);
			Assert.Equal(networkHostId, requeued.NetworkHostId);
			Assert.True(job.QueuedAt < requeued.QueuedAt);
			Assert.Null(requeued.NetworkPortId);
		}

		[Fact]
		public void Requeue_ShouldCreateNewJobWithSameParameters()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkHostId networkHostId = new();
			var job = ToolExecutionJob.ForNetworkHost(networkHostId, ValidJson, manifestId);

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.Equal(job.Parameters.Value, requeued.Parameters.Value);
		}

		[Fact]
		public void Requeue_ShouldCreateNewJobWithSameManifestId()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkHostId networkHostId = new();
			var job = ToolExecutionJob.ForNetworkHost(networkHostId, ValidJson, manifestId);

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.Equal(job.ManifestId, requeued.ManifestId);
		}

		[Fact]
		public void Requeue_ShouldCreateNewJobWithPendingStatus()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkHostId networkHostId = new();
			var job = ToolExecutionJob.ForNetworkHost(networkHostId, ValidJson, manifestId);
			job.Start();
			job.Finish(false, Json.FromString("""{"error":"failed"}"""));

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.Equal(ExecutionStatus.Pending, requeued.Status);
		}

		[Fact]
		public void Requeue_ShouldCreateNewJobWithNullStartedAt()
		{
			// Arrange
			ToolManifestId manifestId = new();
			NetworkHostId networkHostId = new();
			var job = ToolExecutionJob.ForNetworkHost(networkHostId, ValidJson, manifestId);
			job.Start();

			// Act
			var requeued = job.Requeue();

			// Assert
			Assert.Equal(default, requeued.StartedAt);
		}
		#endregion
	}
}