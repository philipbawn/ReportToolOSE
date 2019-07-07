using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models.Documents
{
    public class TokenAwardRecord : MongoDbGenericRepository.Models.Document
    {
        public Guid ReportUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TokensAwarded { get; set; }
        public string TokenModificationAudit { get; set; }
    }
}
