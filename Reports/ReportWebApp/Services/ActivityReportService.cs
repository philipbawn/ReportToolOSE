using MongoDB.Bson;
using MongoDB.Driver;
using ReportWebApp.Helpers;
using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ReportWebApp.Services
{
    public class ActivityReportService : IActivityReportService
    {

        private readonly IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;

        public ActivityReportService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new RepositoryWrapper(_mongoClient);
        }

        public void SaveActivity(Guid reporterId, DateTime activityDate, int minutes, bool ingameActivity, string affectedPeople, string narrative)
        {
            ActivityReport activityReport = new ActivityReport();
            activityReport.ReportUserId = reporterId;
            activityReport.Online = ingameActivity;
            activityReport.ForDate = activityDate;
            activityReport.Affected = affectedPeople;
            activityReport.Minutes = minutes;
            activityReport.Narrative = narrative;
            _wrapper.ActivityReportRepository.AddOne<ActivityReport>(activityReport);
        }

        public int CountActivity(Guid reportUserId, DateTime dateOfCount, bool onlineOnly)
        {
            List<ActivityReport> activityReports = new List<ActivityReport>();

            // Find beginning of month
            var then = dateOfCount;
            var startOfMonth = new DateTime(then.Year, then.Month, 1);

            if (onlineOnly == false)
            {
                // Find all reports between beginning of month and date to count activity.
                activityReports.AddRange(_wrapper.ActivityReportRepository.GetAll<ActivityReport>(f => f.ReportUserId == reportUserId && f.ForDate >= startOfMonth && f.ForDate <= dateOfCount.AddDays(1)).ToList());
            }
            else
            {
                // Find all reports between beginning of month and date to count activity, but only if online.
                activityReports.AddRange(_wrapper.ActivityReportRepository.GetAll<ActivityReport>(f => f.ReportUserId == reportUserId && f.ForDate >= startOfMonth && f.ForDate <= dateOfCount.AddDays(1) && f.Online == true).ToList());
            }
            int minutesActivity = 0;
            foreach (var report in activityReports)
            {
                minutesActivity = minutesActivity + report.Minutes;
            }
            return minutesActivity;
        }

    }
}
