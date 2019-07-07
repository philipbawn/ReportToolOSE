using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Models;
using ReportWebApp.Models.Documents;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Models.ViewModels;
using ReportWebApp.Services;
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
        private readonly IDiscordService _discordService;
        private readonly IActivityReportService _activityReportService;

        public AccountController(IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager, IDiscordService discordService, IActivityReportService activityReportService)
        {
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
            _discordService = discordService;
            _activityReportService = activityReportService;
        }

        public IActionResult Index()
        {
            var viewModel = new AccountIndexViewModel();
            string sessionId = _cookie.Get("ReportSession");
            if (!string.IsNullOrEmpty(sessionId))
            {
                GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    viewModel.CurrentUsername = reportUserByCookie.User.Username;
                    viewModel.AllThisMonth = _activityReportService.CountActivity(reportUserByCookie.User.Id, DateTime.Now, false);
                    viewModel.IngameThisMonth = _activityReportService.CountActivity(reportUserByCookie.User.Id, DateTime.Now, true);
                }
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Display the user profile / preferences alteration page if a person is logged in
        /// </summary>
        public IActionResult Preferences()
        {
            string sessionId = _cookie.Get("ReportSession");
            if (!string.IsNullOrEmpty(sessionId))
            {
                GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    var viewModel = new AccountPreferencesViewModel(reportUserByCookie.User);
                    return View(viewModel);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult DraftReport()
        {
            string sessionId = _cookie.Get("ReportSession");
            if (!string.IsNullOrEmpty(sessionId))
            {
                GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    var viewModel = new AccountDraftReportViewModel();
                    viewModel.CurrentUsername = reportUserByCookie.User.Username;
                    return View(viewModel);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult SaveReport(DateTime forDate, int timeSpent, bool online, string affectedPeople, string narrative)
        {
            string sessionId = _cookie.Get("ReportSession");
            if (!string.IsNullOrEmpty(sessionId))
            {
                GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    var viewModel = new AccountSaveReportViewModel();
                    viewModel.CurrentUsername = reportUserByCookie.User.Username;
                    _activityReportService.SaveActivity(reportUserByCookie.User.Id, forDate, timeSpent, online, affectedPeople, narrative);
                    viewModel.Message = "Saved your activity report.";
                    return View(viewModel);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            var viewModel = new LoginViewModel(_discordService);
            // TODO: re-assess whether we need this?
            // _cookie.Remove("ReportSession");
            return View(viewModel);
        }

        public IActionResult Logout()
        {
            _cookie.Remove("ReportSession");
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string spusername, string sppassword)
        {
            if (_authenticationService.TryLoginCredentials(spusername, sppassword))
            {
                var viewModel = new DoLoginViewModel();
                WebSession session = _authenticationService.CreateWebSession(spusername);
                viewModel.Message = "Created new web session valid until " + session.Expiry.ToShortDateString();
                _cookie.Set("ReportSession", session.SessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(13) });
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
