﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models
{
    public class ReportUser : MongoDbGenericRepository.Models.Document
    {
        public string Rank { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public List<string> OrganizationRoles { get; set; }

    }
}
