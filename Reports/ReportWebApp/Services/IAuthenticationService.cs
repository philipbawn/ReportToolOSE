using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Services
{
    public interface IAuthenticationService
    {

        string HashPassword(string password);
        
        bool VerifyPassword(string password, string storedHash);

        WebSession CreateWebSession(string username);

        GetReportUserByCookieResponse GetReportUserByWebCookie(string cookie);

        bool TryLoginCredentials(string username, string password);

        /// <summary>
        /// Given a user in the report tool and a discord user ID, set the correct property on the report tool user.
        /// </summary>
        /// <param name="reportUserId">Guid representing report tool user ID.</param>
        /// <param name="discordUserId">Discord snowflake ID</param>
        void AssociateUserWithDiscordId(Guid reportUserId, string discordUserId);
        string CreateWebSessionFromDiscordId(string discordUserId);

        /// <summary>
        /// Determine whether or not the system should auto-create users beyond the first one.
        /// </summary>
        /// <returns></returns>
        bool SystemShouldAutoCreateAccounts();
    }
}
