﻿using MongoDB.Driver;
using ReportWebApp.Helpers;
using ReportWebApp.Models;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ReportwebApp.Services
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
                ReportUser newUser = new ReportUser();
                newUser.PasswordHash = HashPassword(password);
                newUser.Username = username;
                _wrapper.ReportUserRepository.AddOne<ReportUser>(newUser);
                user = newUser;
            }
            if (VerifyPassword(password, user.PasswordHash))
            {
                return true;
            }
            return false;

        }

    }
}
