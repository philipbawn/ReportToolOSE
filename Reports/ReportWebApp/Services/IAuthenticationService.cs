using ReportWebApp.Models;
using ReportWebApp.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportwebApp.Services
{
    public interface IAuthenticationService
    {

        string HashPassword(string password);
        
        bool VerifyPassword(string password, string storedHash);

        WebSession CreateWebSession(string username);

        GetReportUserByCookieResponse GetReportUserByWebCookie(string cookie);

        bool TryLoginCredentials(string username, string password);
    }
}
