namespace ReportDistributorService.Response
{
    using Newtonsoft.Json;

    public class EmailResponse 
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}