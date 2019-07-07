using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ServiceResponse
{
    public class SaveReportResponse
    {
        public bool Success { get; set; }

        public SaveReportResponse(bool success)
        {
            this.Success = success;
        }
    }
}
