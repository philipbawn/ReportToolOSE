using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Controllers
{
    public class DiscordController : Controller
    {
        private readonly IAuthenticationService authService;
        private readonly IDiscordService discordService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;

        public DiscordController(IAuthenticationService authenticationService, IDiscordService discordService, ICookieManager cookieManager, ICookie cookie)
        {
            this.authService = authenticationService;
            this.discordService = discordService;
            _cookie = cookie;
            _cookieManager = cookieManager;
        }

        [Route("/signin-discord")]
        public IActionResult SigninWithDiscord([FromQuery] string code)
        {
            string sessionId = _cookie.Get("ReportSession");
            var viewModel = new ReportWebApp.Models.ViewModels.Discord.Signin(this.discordService, code, this.authService, sessionId);
            _cookie.Set("ReportSession", viewModel.NewSessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(13) });

            return View(viewModel);
        }

    }
}
