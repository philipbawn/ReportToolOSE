using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Documents
{
    public class ApplicationSetting : MongoDbGenericRepository.Models.Document
    {
        /// <summary>
        /// Set this property to 'Default' to apply automatically. Set this property to something else to act as a template.
        /// </summary>
        public string SettingsType { get; set; }
        public bool AutoCreateAccountsForAnyone { get; set; }
    }
}
