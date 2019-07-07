using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ViewModels
{
    public class AccountIndexViewModel
    {
        public string CurrentUsername { get; set; }
        public int AllThisMonth { get; set; }
        public int IngameThisMonth { get; set; }
    }
}
