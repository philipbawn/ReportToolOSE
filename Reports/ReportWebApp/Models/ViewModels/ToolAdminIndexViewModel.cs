using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ViewModels
{
    public class ToolAdminIndexViewModel
    {
        public bool IsToolAdmin { get; set; }

        public ToolAdminIndexViewModel(IAuthenticationService authenticationService, string sessionId)
        {

        }
    }
}
