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

        /// <summary>
        /// Save an activity report
        /// </summary>
        /// <param name="reporterId">Uniquely identifies the reporter of the activity</param>
        /// <param name="activityDate">The date and time the activity occurred. Only date part is required, time ignored for the most part.</param>
        /// <param name="minutes"># Minutes spent by the volunteer</param>
        /// <param name="ingameActivity">Whether or not the activity happened online (in a game) or not</param>
        /// <param name="affectedPeople">People affected by the volunteer activity</param>
        /// <param name="narrative">A narrative written by the reporter.</param>
        void SaveActivity(Guid reporterId, DateTime activityDate, int minutes, bool ingameActivity, string affectedPeople, string narrative);
    }
}
