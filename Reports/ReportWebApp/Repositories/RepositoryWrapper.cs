using MongoDB.Driver;
using ReportWebApp.Models.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IMongoClient _mongoClient;

        private IRepositoryBase<ReportUser> reportUserRepository;
        private IRepositoryBase<WebSession> webSessionRepository;
        private IRepositoryBase<ApplicationSetting> applicationSettingRepository;

        public RepositoryWrapper(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IRepositoryBase<ReportUser> ReportUserRepository
        {
            get
            {
                if (this.reportUserRepository == null)
                {
                    this.reportUserRepository = new RepositoryBase<ReportUser>(_mongoClient);
                }
                return this.reportUserRepository;
            }
        }

        public IRepositoryBase<WebSession> WebSessionRepository
        {
            get
            {
                if (this.webSessionRepository == null)
                {
                    this.webSessionRepository = new RepositoryBase<WebSession>(_mongoClient);
                }
                return this.webSessionRepository;
            }
        }

        public IRepositoryBase<ApplicationSetting> ApplicationSettingRepository
        {
            get
            {
                if (this.applicationSettingRepository == null)
                {
                    this.applicationSettingRepository = new RepositoryBase<ApplicationSetting>(_mongoClient);
                }
                return this.applicationSettingRepository;
            }
        }
    }
}
