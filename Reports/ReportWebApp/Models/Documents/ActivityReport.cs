using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Documents
{
    public class ActivityReport : MongoDbGenericRepository.Models.Document
    {
        public Guid ReportUserId { get; set; }
        public DateTime ForDate { get; set; }
        public int Minutes { get; set; }
        public bool Online { get; set; }
        public string Affected { get; set; }
        public string Narrative { get; set; }
    }
}
