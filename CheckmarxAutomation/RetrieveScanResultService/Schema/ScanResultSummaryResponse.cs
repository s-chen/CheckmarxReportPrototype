using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RetrieveScanResultService.Schema
{
    [Serializable]
    public class ScanResultSummaryResponse
    {
        [JsonProperty("value")]
        public List<Value> Values { get; set; }

        public class Value
        {
            [JsonProperty("SastLastScan")]
            public SastLastScan SastScan { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("OwningTeam")]
            public string OwningTeam { get; set; }
        }

        public class SastLastScan
        {
            [JsonProperty("HighVulnerabilities")]
            public string HighVulnerabilities { get; set; }

            [JsonProperty("MediumVulnerabilities")]
            public string MediumVulnerabilities { get; set; }

            [JsonProperty("LowVulnerabilities")]
            public string LowVulnerabilities { get; set; }

            [JsonProperty("InfoVulnerabilities")]
            public string InfoVulnerabilities { get; set; }

            [JsonProperty("ScanRequestCompletedOn")]
            public string ScanRequestCompletedOn { get; set; }
        }
    }
}