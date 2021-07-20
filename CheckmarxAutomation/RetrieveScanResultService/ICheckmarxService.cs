using System.Threading;
using System.Threading.Tasks;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Schema;

namespace RetrieveScanResultService
{
    public interface ICheckmarxService
    {
        Task<ScanResultSummaryResponse> GetProjectScanSummaryAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken);

        Task<ScanResultDetailResponse> GetProjectScanDetailAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken);
    }
}