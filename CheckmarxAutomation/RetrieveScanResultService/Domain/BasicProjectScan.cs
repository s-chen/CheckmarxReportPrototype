namespace RetrieveScanResultService.Domain
{
    public class BasicProjectScan
    {
        public string HighVulnerabilities { get; set; }

        public string MediumVulnerabilities { get; set; }

        public string LowVulnerabilities { get; set; }

        public string InfoVulnerabilities { get; set; }

        public string ProjectName { get; set; }

        public string OwningTeam { get; set; }

        public string ScanRequestCompletion { get; set; }
    }
}