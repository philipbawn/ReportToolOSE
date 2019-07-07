using Microsoft.Extensions.Configuration;
using System;

namespace ReportWebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string SentryDsn { get; set; }
        public ErrorViewModel(IConfiguration configuration)
        {
            SentryDsn = configuration["Sentry:Dsn"];
        }
    }
}