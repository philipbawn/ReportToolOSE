using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Services
{
    public interface IDiscordService
    {
        string GetSigninUri(string clientId, string redirectUri, string spaceDelimitedScopes);
    }
}
