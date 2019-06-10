using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Discord
{
    public class RefreshTokenForTokenRequest
    {

        public RefreshTokenForTokenRequest(string clientId, string clientSecret, string refreshToken, string redirectUri, string scope)
        {
            this.client_id = clientId;
            this.client_secret = clientSecret;
            this.refresh_token = refreshToken;
            this.grant_type = "refresh_token";
            this.redirect_uri = redirectUri;
            this.scope = scope;
        }

        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
    }
}
