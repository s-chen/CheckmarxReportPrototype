using System.Threading;
using System.Threading.Tasks;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Schema;

namespace RetrieveScanResultService
{
    //// This service is based on: https://checkmarx.atlassian.net/wiki/spaces/KC/pages/339247929/Get+Last+Scan+by+Project+Id
    public class CheckmarxService : ICheckmarxService
    {
        public Task<ScanResultDetailResponse> GetProjectScanDetailAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScanResultSummaryResponse> GetProjectScanSummaryAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken)
        {
            ////TODO: Integrate with real Checkmarx API when available
            throw new System.NotImplementedException();
        }
    }
}