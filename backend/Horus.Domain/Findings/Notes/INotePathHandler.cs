using Horus.Domain.Scanning.NetworkHosts;
using Horus.Domain.Scanning.ScanTargets;
using Horus.Domain.SharedKernel.FilePaths;

namespace Horus.Domain.Findings.Notes
{
	public interface INotePathHandler
	{
		public FilePath CreateForNetworkHost(NoteId id, NetworkHostId networkHostId);
		public FilePath CreateForScanTarget(NoteId id, ScanTargetId scanTargetId);
	}
}