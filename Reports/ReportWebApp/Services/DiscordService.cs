using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public DiscordService(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string GetSigninUri(string clientId, string redirectUri, string spaceDelimitedScopes)
        {
            return string.Concat("https://discordapp.com/api/oauth2/authorize?client_id=", clientId, "&redirect_uri=", Flurl.Url.Encode(redirectUri), "&response_type=code&scope=", Flurl.Url.Encode(spaceDelimitedScopes));
        }
    }
}
