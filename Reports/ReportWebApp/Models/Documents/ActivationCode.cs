using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Documents
{
    public class ActivationCode : MongoDbGenericRepository.Models.Document
    {
        public DateTime ExpirationDateUtc { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
    }
}
