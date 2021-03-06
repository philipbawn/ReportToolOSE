﻿using ReportWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ViewModels.Home
{
    public class HomeIndex
    {
        public HomeIndex(IDiscordService discordService)
        {
            this.DiscordSigninUrl = discordService.GetSigninUri();
        }
        public string DiscordSigninUrl { get; set; }

        public bool NeedToSignIn { get; set; }
    }
}
