using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ReportGeneratorService.Enums;
using RetrieveScanResultService.Domain;

namespace ReportGeneratorService
{
    public class ReportGeneratorService : IReportGeneratorService<BasicProjectScan>
    {
        public async Task<ServiceStatus> CreateReportAsync(BasicProjectScan data, CancellationToken cancellationToken)
        {
            var status = ServiceStatus.Failed;

            var records = new List<BasicProjectScan> { data };
            var filePath = @"C:\\Stash\\CheckmarxReportDemo\\CheckmarxAutomation\\test.csv";
            try
            {
                using (var mem = new MemoryStream())
                using (var writer = new StreamWriter(mem))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";

                    csvWriter.WriteField("Project name");
                    csvWriter.WriteField("Owner");
                    csvWriter.WriteField("Scan completed on");
                    csvWriter.WriteField("High");
                    csvWriter.WriteField("Medium");
                    csvWriter.WriteField("Low");
                    csvWriter.WriteField("Info");
                    csvWriter.NextRecord();

                    foreach (var item in records)
                    {
                        csvWriter.WriteField(item.ProjectName);
                        csvWriter.WriteField(item.OwningTeam);
                        csvWriter.WriteField(item.ScanRequestCompletion);
                        csvWriter.WriteField(item.HighVulnerabilities);
                        csvWriter.WriteField(item.MediumVulnerabilities);
                        csvWriter.WriteField(item.LowVulnerabilities);
                        csvWriter.WriteField(item.InfoVulnerabilities);
                        csvWriter.NextRecord();
                    }


                    writer.Flush();
                    var result = Encoding.UTF8.GetString(mem.ToArray());
                    File.AppendAllText(filePath, result);
                }

                status = ServiceStatus.Success;
                return status;
            }
            catch (Exception ex)
            {
                // catch exception
            }

            return status;
        }
    }
}
