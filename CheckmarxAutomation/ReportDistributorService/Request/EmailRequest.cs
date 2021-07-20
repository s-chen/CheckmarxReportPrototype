namespace ReportDistributorService.Request
{
    using System;
    using Newtonsoft.Json;
    using ReportDistributorService.Request.Request;

    public class EmailRequest
    {
        [JsonProperty("template-id")]
        public string TemplateId { get; set; }

        [JsonProperty("affiliate-id")]
        public string AffiliateId { get; set; }

        [JsonProperty("recipient-email")]
        public string RecipientEmail { get; set; }

        [JsonProperty("template-fields")]
        public TemplateFields[] TemplateFields { get; set; }
    }   
}
