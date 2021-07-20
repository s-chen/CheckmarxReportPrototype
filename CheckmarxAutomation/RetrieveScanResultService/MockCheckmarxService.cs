using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Schema;

namespace RetrieveScanResultService
{
    //// This service is based on: https://checkmarx.atlassian.net/wiki/spaces/KC/pages/339247929/Get+Last+Scan+by+Project+Id
    public class MockCheckmarxService : ICheckmarxService
    {
        public Task<ScanResultSummaryResponse> GetProjectScanSummaryAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken)
        {
            var json = LoadJsonResponse(context.FileName);
            var response = JsonConvert.DeserializeObject<ScanResultSummaryResponse>(json);

            return Task.FromResult(response);
        }

        public Task<ScanResultDetailResponse> GetProjectScanDetailAsync(ProjectScanSummaryContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static string LoadJsonResponse(string fileName)
        {
            var fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Responses\{0}.json", fileName));
            using (var reader = new StreamReader(fileFullPath))
            {
                var json = reader.ReadToEnd();
                return json;
            }
        }
    }
}