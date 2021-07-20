namespace ReportDistributorService
{
    using Flurl.Http;
    using Flurl.Http.Configuration;
    using Newtonsoft.Json;
    using ReportDistributorService.Context;
    using ReportDistributorService.Exception;
    using ReportDistributorService.Options;
    using ReportDistributorService.Request;
    using ReportDistributorService.Request.Request;
    using ReportDistributorService.Response;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions options;
        private readonly IFlurlClientFactory flurlClientFactory;

        public EmailService(EmailServiceOptions options, IFlurlClientFactory flurlClientFactory)
        {
            this.options = options;
            this.flurlClientFactory = flurlClientFactory;
        }


        public async Task<EmailResponse> SendEmailAsync(EmailContext context, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(this.options.Endpoint.Url))
            {
                throw new ArgumentException("EmailService Configuration service endpoint not defined");
            }

            var url = string.Format(this.options.Endpoint.Url);
            var flurlClient = this.flurlClientFactory.Get(url);
            var flurlRequest = flurlClient.Request()
                .WithBasicAuth(this.options.Endpoint.Username, this.options.Endpoint.Password)
                .WithHeader("RequestingSystem", this.options.Endpoint.RequestingSystem);

            var contentTemplate = this.CreateRequest(context);

            try
            {
                using (var response = await flurlRequest
                    .PostStringAsync(contentTemplate, cancellationToken)
                    .ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var emailResponse = JsonConvert.DeserializeObject<EmailResponse>(responseContent);
                    return emailResponse;
                }
            }
            catch (System.Exception ex)
            {
                throw new EmailServiceException($"Error processing email service", ex);
            }
        }

        private string CreateRequest(EmailContext context)
        {
            var emailRequest = new EmailRequest
            {
                TemplateId = context.TemplateId,
                RecipientEmail = context.RecipientEmail,
                AffiliateId = context.AffiliateId,
                TemplateFields = MapTemplateFields(context.TemplateFields)
            };

            return JsonConvert.SerializeObject(emailRequest);
        }

        private TemplateFields[] MapTemplateFields(IEnumerable<Models.TemplateFields> fields)
        {
            var templateFields = new List<TemplateFields>();
            foreach (var item in fields)
            {
                templateFields.Add(new TemplateFields { Id = item.Id, Value = item.Value });
            }

            return templateFields.ToArray();
        }
    }
}

