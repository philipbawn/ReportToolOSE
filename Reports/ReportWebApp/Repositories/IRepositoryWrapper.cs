using ReportWebApp.Models.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<ApplicationSetting> ApplicationSettingRepository { get; }
        IRepositoryBase<ActivityReport> ActivityReportRepository { get; }
        IRepositoryBase<ReportUser> ReportUserRepository { get; }
        IRepositoryBase<TokenAwardRecord> TokenAwardRecordRepository { get; }
        IRepositoryBase<WebSession> WebSessionRepository { get; }

    }
}
