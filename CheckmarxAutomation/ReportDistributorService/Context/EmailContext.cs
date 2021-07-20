using ReportDistributorService.Models;
using ReportDistributorService.Request;
using System.Collections.Generic;

namespace ReportDistributorService.Context
{
    public class EmailContext
    {        
        public string TemplateId { get; set; }
       
        public string AffiliateId { get; set; }
     
        public string RecipientEmail { get; set; }

        public IEnumerable<TemplateFields> TemplateFields { get; set; }
    }
}
