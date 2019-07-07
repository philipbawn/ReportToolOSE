using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CookieManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReportWebApp.Models;
using ReportWebApp.Models.ServiceResponse;
using ReportWebApp.Services;

namespace ReportWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService authService;
        private readonly IDiscordService discordService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;
        private IConfiguration _configuration;

        public HomeController(IAuthenticationService authenticationService, IDiscordService discordService, ICookie cookie, ICookieManager cookieManager, IConfiguration configuration)
        {
            this.authService = authenticationService;
            this.discordService = discordService;
            _cookie = cookie;
            _cookieManager = cookieManager;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            var viewModel = new ReportWebApp.Models.ViewModels.Home.HomeIndex(this.discordService);

            string sessionId = _cookie.Get("ReportSession");
            GetReportUserByCookieResponse reportUserByCookie = authService.GetReportUserByWebCookie(sessionId);
            if (reportUserByCookie.Success == true)
            {
                viewModel.NeedToSignIn = false;
            }
            else
            {
                viewModel.NeedToSignIn = true;
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel(_configuration) { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
