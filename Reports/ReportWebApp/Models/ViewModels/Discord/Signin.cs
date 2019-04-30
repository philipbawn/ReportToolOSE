using ReportWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ReportWebApp.Models.Discord;
using ReportwebApp.Services;

namespace ReportWebApp.Models.ViewModels.Discord
{
    public class Signin
    {
        public string Message { get; set; }
        
        public Signin(IDiscordService discordService, string code, IAuthenticationService authenticationService, string sessionId)
        {
            AccessTokenResponse response = discordService.GetAccessTokenWithCode(code);
            Message = response.access_token + " expires in " + response.expires_in;
        }
    }
}
