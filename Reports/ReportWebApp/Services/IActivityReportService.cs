using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Services
{
    public interface IActivityReportService
    {

        /// <summary>
        /// Count how much activity (in minutes) a certain user has
        /// </summary>
        /// <param name="reportUserId">The user who</param>
        /// <param name="dateOfCount"></param>
        /// <returns></returns>
        int CountActivity(Guid reportUserId, DateTime dateOfCount, bool onlineOnly);
    }
}
