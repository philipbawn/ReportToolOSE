using ReportWebApp.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.ViewModels
{
    public class AccountPreferencesViewModel
    {
        private readonly ReportUser user;

        public ReportUser User {
            get {
                return user;
            }
        }

        public AccountPreferencesViewModel(ReportUser user)
        {
            this.user = user;
        }
    }
}
