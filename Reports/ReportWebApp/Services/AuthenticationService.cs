using MongoDB.Bson;
using MongoDB.Driver;
using ReportWebApp.Helpers;
using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ReportWebApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public AuthenticationService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public string HashPassword(string password)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32);
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            return rfc2898DeriveBytes.IterationCount + "|" + Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(storedHash.Split('|')[1]));
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            if (Convert.ToBase64String(hash) == storedHash.Split('|')[2])
            {
                return true;
            }
            return false;
        }

        public string CreateWebSessionFromDiscordId(string discordUserId)
        {
            ReportUser reportUser = _wrapper.ReportUserRepository.GetOne<ReportUser>(f => f.DiscordId == discordUserId);
            if (reportUser != null)
            {
                WebSession webSession = this.CreateWebSession(reportUser.Username);
                return webSession.SessionCookie;
            }
            else
            {
                if (SystemShouldAutoCreateAccounts())
                {
                    ReportUser newUser = new ReportUser();
                    newUser.PasswordHash = HashPassword(GenerationHelper.CreateRandomString(true, true, false, 20));
                    newUser.Username = discordUserId;
                    newUser.IsOrganizationAdmin = false;
                    newUser.OrganizationRoles = new List<string>();
                    _wrapper.ReportUserRepository.AddOne<ReportUser>(newUser);
                    WebSession webSession = this.CreateWebSession(newUser.Username);
                    return webSession.SessionCookie;
                }
                else
                {
                    return "";
                }
            }
        }


        /// <summary>
        /// Creates a new web session and return it for a given user.
        /// </summary>
        /// <param name="username">The username for which the session is for.</param>
        /// <returns>A WebSession</returns>
        public WebSession CreateWebSession(string username)
        {
            WebSession webSession = new WebSession();
            ReportUser reportUser = _wrapper.ReportUserRepository.GetOne<ReportUser>(f => f.Username == username);
            if (reportUser != null)
            {
                webSession.Expiry = DateTime.UtcNow.AddDays(14);
                webSession.ReportUserId = reportUser.Id;
                webSession.ForReportUsername = username;
                webSession.SessionCookie = GenerationHelper.CreateRandomString(true, true, false, 32);
                _wrapper.WebSessionRepository.AddOne<WebSession>(webSession);
            }
            return webSession;
        }

        public GetReportUserByCookieResponse GetReportUserByWebCookie(string cookie)
        {
            GetReportUserByCookieResponse result = new GetReportUserByCookieResponse();
            WebSession webSession = _wrapper.WebSessionRepository.GetOne<WebSession>(f => f.SessionCookie == cookie);
            if (webSession != null && webSession.Expiry > DateTime.UtcNow)
            {
                result.User = _wrapper.ReportUserRepository.GetOne<ReportUser>(f => f.Id == webSession.ReportUserId);
                result.Success = true;
            }
            else
            {
                result.User = null;
                result.Success = false;
            }
            return result;
        }

        public bool TryLoginCredentials(string username, string password)
        {
            ReportUser user = _wrapper.ReportUserRepository.GetOne<ReportUser>(f => f.Username == username);
            if (user == null)
            {
                long usercount = _wrapper.ReportUserRepository.Count<ReportUser>(c => c.Id != null);
                if (usercount == 0)
                {
                    ReportUser newUser = new ReportUser();
                    newUser.PasswordHash = HashPassword(password);
                    newUser.Username = username;
                    newUser.IsOrganizationAdmin = true;
                    newUser.OrganizationRoles = new List<string>();
                    newUser.OrganizationRoles.Add("ToolAdmin");
                    _wrapper.ReportUserRepository.AddOne<ReportUser>(newUser);
                    user = newUser;
                }
                else
                {
                    if (SystemShouldAutoCreateAccounts())
                    {
                        ReportUser newUser = new ReportUser();
                        newUser.PasswordHash = HashPassword(password);
                        newUser.Username = username;
                        newUser.IsOrganizationAdmin = false;
                        newUser.OrganizationRoles = new List<string>();
                        _wrapper.ReportUserRepository.AddOne<ReportUser>(newUser);
                        user = newUser;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (VerifyPassword(password, user.PasswordHash))
            {
                return true;
            }
            return false;

        }

        public void AssociateUserWithDiscordId(Guid reportUserId, string discordUserId)
        {
            var existing = _wrapper.ReportUserRepository.GetOne<ReportUser>(f => f.Id == reportUserId);
            existing.DiscordId = discordUserId;
            _wrapper.ReportUserRepository.UpdateOne<ReportUser>(existing);
        }

        public bool SystemShouldAutoCreateAccounts()
        {
            var settings = _wrapper.ApplicationSettingRepository.GetOne<ApplicationSetting>(f => f.SettingsType == "Default");
            if (settings != null && settings.AutoCreateAccountsForAnyone == true)
            {
                return true;
            }
            return false;
        }

    }
}
