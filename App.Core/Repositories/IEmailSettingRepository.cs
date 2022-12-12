using App.Core.Models;

namespace App.Core.Repositories
{
    public interface IEmailSettingRepository : IGenericRepository<EmailSetting>
    {
        Task<EmailSetting> GetByContentType(SendingContentType sendingContentType);
    }
}
