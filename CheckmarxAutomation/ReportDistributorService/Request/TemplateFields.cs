namespace ReportDistributorService.Request.Request
{
    using Newtonsoft.Json;

    public class TemplateFields
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
