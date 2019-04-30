using ReportWebApp.Models.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace ReportWebApp.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly string _tokenUri;

        public DiscordService(string clientId, string clientSecret, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
            _tokenUri = "https://discordapp.com/api/oauth2/token";
        }

        public string GetSigninUri()
        {
            return string.Concat("https://discordapp.com/api/oauth2/authorize?client_id=", _clientId, "&redirect_uri=", Flurl.Url.Encode(_redirectUri), "&response_type=code&scope=", Flurl.Url.Encode("identify guilds"));
        }

        public AccessTokenResponse GetAccessTokenWithCode(string code)
        {
            CodeForTokenRequest request = new CodeForTokenRequest(_clientId, _clientSecret, code, _redirectUri, Flurl.Url.Encode("identify guilds"));
            var httpResponse = _tokenUri.PostUrlEncodedAsync(request).Result;
            AccessTokenResponse response = JsonConvert.DeserializeObject<AccessTokenResponse>(httpResponse.Content.ReadAsStringAsync().Result);
            return response;
        }
    }
}
