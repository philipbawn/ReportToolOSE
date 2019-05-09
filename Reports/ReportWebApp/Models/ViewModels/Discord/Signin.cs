using ReportWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ReportWebApp.Models.Discord;
using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;

namespace ReportWebApp.Models.ViewModels.Discord
{
    public class Signin
    {
        public string Message { get; set; }
        public string NewSessionCookie { get; set; }
        public Signin(IDiscordService discordService, string code, IAuthenticationService authenticationService, string sessionId)
        {
            AccessTokenResponse response = discordService.GetAccessTokenWithCode(code);
            MeResponse discordUser = discordService.GetMeResponse(response.access_token);
            GetReportUserByCookieResponse tryRetrieveUser = authenticationService.GetReportUserByWebCookie(sessionId);
            if (tryRetrieveUser.Success == true)
            {
                Message = "You were logged in already, this site has tried to associate your discord ID and report tool account (" + tryRetrieveUser.User.Username + ").";
                authenticationService.AssociateUserWithDiscordId(tryRetrieveUser.User.Id, discordUser.id);
                this.NewSessionCookie = authenticationService.CreateWebSessionFromDiscordId(discordUser.id);
            }
            else
            {
                Message = "You were not logged in to a report tool account before utilizing discord as a signin service.";
                this.NewSessionCookie = authenticationService.CreateWebSessionFromDiscordId(discordUser.id);
                if (this.NewSessionCookie == string.Empty)
                {
                    Message = "This site does not permit auto-creation of users. Seek out a tool administrator's assistance with registration.";
                }
            }
        }
    }
}
