using Microsoft.Diagnostics.EventFlow;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp
{
    public class CustomFilter : IFilter
    {
        private IHealthReporter HealthReporter;
        private string MachineName;
        public CustomFilter(string ServerName, IHealthReporter HealthReporter)
        {
            MachineName = ServerName;
            this.HealthReporter = HealthReporter;
        }

        FilterResult IFilter.Evaluate(EventData eventData)
        {
            // TODO: check the eventData object to see if the event data involves a dependency
            // call to the telemetry collector, and return
            // FilterResult.DiscardEvent instead of KeepEvent
            if (eventData.Payload["TelemetryType"].ToString() == "dependency" && eventData.Payload["Name"].ToString().StartsWith("POST") && eventData.Payload["Name"].ToString().EndsWith("/api/Collect"))
            {
                return FilterResult.DiscardEvent;
            }
            else
            {
                eventData.AddPayloadProperty("ServerName", MachineName, HealthReporter, "CustomFilter");
                return FilterResult.KeepEvent;
            }
        }
    }

    public class CustomFilterFactory : IPipelineItemFactory<CustomFilter>
    {
        public CustomFilter CreateItem(IConfiguration configuration, IHealthReporter healthReporter)
        {
            CustomFilter filter = new CustomFilter(System.Environment.MachineName, healthReporter);
            return filter;
        }
    }
}
