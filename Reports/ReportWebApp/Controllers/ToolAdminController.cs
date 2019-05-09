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
    public class ToolAdminController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;

        public ToolAdminController(IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            string sessionId = _cookie.Get("ReportSession");

            GetReportUserByCookieResponse reportUserByCookie = _authenticationService.GetReportUserByWebCookie(sessionId);
            if (reportUserByCookie.Success == true && reportUserByCookie.User.OrganizationRoles.Contains("ToolAdmin"))
            {
                var viewModel = new ReportWebApp.Models.ViewModels.ToolAdminIndexViewModel(_authenticationService, sessionId);
                viewModel.IsToolAdmin = true;
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
