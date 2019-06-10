using ReportWebApp.Models.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<ReportUser> ReportUserRepository { get; }
        IRepositoryBase<WebSession> WebSessionRepository { get; }

        IRepositoryBase<ApplicationSetting> ApplicationSettingRepository { get; }

    }
}
