using App.Core.Models;

namespace App.Core.Services
{
    public interface IEmailSettingService : IGenericService<EmailSetting>
    {
        Task<EmailSetting> GetByContentType(SendingContentType sendingContentType);
    }
}
