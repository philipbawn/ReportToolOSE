using ReportWebApp.Services;
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
            this.Message = discordService.GetSigninUri();
        }
        public string Message { get; set; }
    }
}
