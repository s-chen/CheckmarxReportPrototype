using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Domain;
using RetrieveScanResultService.Schema;

namespace RetrieveScanResultService
{
    public class RetrieveScanResultService : IRetrieveScanResultService
    {
        private readonly ICheckmarxService checkmarxService;

        public RetrieveScanResultService(ICheckmarxService checkmarxService)
        {
            this.checkmarxService = checkmarxService;
        }

        public async Task<BasicProjectScan> GetProjectBasicScanAsync(GetScanContext context, CancellationToken cancellationToken)
        {
            var summaryScanContext = new ProjectScanSummaryContext { FileName = context.Filename };
            var response = await this.checkmarxService.GetProjectScanSummaryAsync(summaryScanContext, cancellationToken);

            return this.MapResponse(response);
        }

        private BasicProjectScan MapResponse(ScanResultSummaryResponse response)
        {
            var model = new BasicProjectScan
            {
                HighVulnerabilities = response.Values.First().SastScan.HighVulnerabilities,
                MediumVulnerabilities = response.Values.First().SastScan.MediumVulnerabilities,
                LowVulnerabilities = response.Values.First().SastScan.LowVulnerabilities,
                InfoVulnerabilities = response.Values.First().SastScan.InfoVulnerabilities,
                ScanRequestCompletion = response.Values.First().SastScan.ScanRequestCompletedOn,
                ProjectName = response.Values.First().Name,
                OwningTeam = response.Values.First().OwningTeam
            };

            return model;
        }
    }
}
