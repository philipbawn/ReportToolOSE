﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Discord
{
    public class CodeForTokenRequest
    {

        public CodeForTokenRequest(string clientId, string clientSecret, string code, string redirectUri, string scope)
        {
            this.client_id = clientId;
            this.client_secret = clientSecret;
            this.code = code;
            this.grant_type = "authorization_code";
            this.redirect_uri = redirectUri;
            this.scope = scope;
        }

        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
    }
}
