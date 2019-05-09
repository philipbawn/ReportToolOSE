using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Models;
using ReportWebApp.Services;

namespace ReportWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService authService;
        private readonly IDiscordService discordService;

        public HomeController(IAuthenticationService authenticationService, IDiscordService discordService)
        {
            this.authService = authenticationService;
            this.discordService = discordService;
        }

        public IActionResult Index()
        {
            var viewModel = new ReportWebApp.Models.ViewModels.Home.HomeIndex(this.discordService);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
