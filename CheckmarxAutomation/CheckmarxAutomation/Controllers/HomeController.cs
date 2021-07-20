using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckmarxAutomation.Models;
using ReportGeneratorService;
using ReportGeneratorService.Enums;
using RetrieveScanResultService;
using RetrieveScanResultService.Context;
using RetrieveScanResultService.Domain;

namespace CheckmarxAutomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRetrieveScanResultService retrieveService;
        private readonly IReportGeneratorService<BasicProjectScan> generatorService;

        public HomeController(IRetrieveScanResultService retrieveService, IReportGeneratorService<BasicProjectScan> generatorService)
        {
            this.retrieveService = retrieveService;
            this.generatorService = generatorService;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }

        public async Task<ViewResult> CreateReport()
        {
            var context = new GetScanContext { Filename = "ProjectScanSummaryResponse" };
            var data = await this.retrieveService.GetProjectBasicScanAsync(context, new CancellationToken());

            var status = await this.generatorService.CreateReportAsync(data, new CancellationToken());

            if (status == ServiceStatus.Success)
            {
                return this.View("CreateReportSuccess");
            }

            return this.View("CreateReportError");
        }

        public ActionResult SendReport()
        {
            return null;
        }
    }
}
