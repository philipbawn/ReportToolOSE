using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Discord
{
    public class MeResponse
    {
        public string username { get; set; }
        public string locale { get; set; }
        public bool mfa_enabled { get; set; }
        public int flags;
        public string avatar;
        public string discriminator { get; set; }
        public string id { get; set; }
    }
}