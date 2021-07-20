using System.Threading;
using System.Threading.Tasks;
using ReportGeneratorService.Contexts;
using ReportGeneratorService.Enums;

namespace ReportGeneratorService
{
    public interface IReportGeneratorService<T>
    {
        Task<ServiceStatus> CreateReportAsync(T data, CancellationToken cancellationToken);
    }
}