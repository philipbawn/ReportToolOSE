using ReportWebApp.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ServiceResponse
{
    public class GetReportUserByCookieResponse
    {
            public ReportUser User { get; set; }
            public bool Success { get; set; }
    }
}
