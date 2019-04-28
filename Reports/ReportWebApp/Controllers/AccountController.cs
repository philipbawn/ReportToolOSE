using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportwebApp.Services;
using ReportWebApp.Models;
using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;

        public AccountController(IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            var viewModel = new AccountIndexViewModel();
            string sessionId = _cookie.Get("ReportSession");
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    viewModel.Message = "Welcome, " + reportUserByCookie.User.Username;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            _cookie.Remove("ReportSession");
            return View();
        }

        public IActionResult Logout()
        {
            _cookie.Remove("ReportSession");
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string spusername, string sppassword)
        {
            var viewModel = new DoLoginViewModel();

            if (_authenticationService.TryLoginCredentials(spusername, sppassword))
            {
                WebSession session = _authenticationService.CreateWebSession(spusername);
                viewModel.Message = "Created new web session valid until " + session.Expiry.ToShortDateString();
                _cookie.Set("ReportSession", session.SessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(13) });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
