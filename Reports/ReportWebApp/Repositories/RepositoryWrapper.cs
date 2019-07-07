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

        private IRepositoryBase<ApplicationSetting> applicationSettingRepository;
        private IRepositoryBase<ActivityReport> activityReportRepository;
        private IRepositoryBase<ReportUser> reportUserRepository;
        private IRepositoryBase<TokenAwardRecord> tokenAwardRecordRepository;
        private IRepositoryBase<WebSession> webSessionRepository;

        public RepositoryWrapper(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IRepositoryBase<ActivityReport> ActivityReportRepository
        {
            get
            {
                if (this.activityReportRepository == null)
                {
                    this.activityReportRepository = new RepositoryBase<ActivityReport>(_mongoClient);
                }
                return this.activityReportRepository;
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

        public IRepositoryBase<TokenAwardRecord> TokenAwardRecordRepository
        {
            get
            {
                if (this.tokenAwardRecordRepository == null)
                {
                    this.tokenAwardRecordRepository = new RepositoryBase<TokenAwardRecord>(_mongoClient);
                }
                return this.tokenAwardRecordRepository;
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
    }
}
