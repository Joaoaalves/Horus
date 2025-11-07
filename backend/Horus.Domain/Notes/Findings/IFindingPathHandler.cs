using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SharedKernel.FilePaths;

namespace Horus.Domain.Notes.Findings
{
	public interface IFindingPathHandler
	{
		public FilePath CreateForNetworkHost(FindingId id, NetworkHostId networkHostId);
		public FilePath CreateForScanTarget(FindingId id, ScanTargetId scanTargetId);
	}
}