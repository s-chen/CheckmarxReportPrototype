using System.Threading;
using System.Threading.Tasks;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Domain;

namespace RetrieveScanResultService
{
    public interface IRetrieveScanResultService
    {
        Task<BasicProjectScan> GetProjectBasicScanAsync(GetScanContext context, CancellationToken cancellationToken);
    }
}
