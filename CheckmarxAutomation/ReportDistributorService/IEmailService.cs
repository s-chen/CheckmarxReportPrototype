using ReportDistributorService.Context;
using ReportDistributorService.Request;
using ReportDistributorService.Response;
using System.Threading;
using System.Threading.Tasks;

namespace ReportDistributorService
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailAsync(EmailContext emailRequest, CancellationToken cancellationToken);
    }
}
